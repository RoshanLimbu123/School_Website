using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Helper;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using System;

namespace CMS.Core.Service.Implementation
{
    public class PageCategoryServiceImpl : PageCategoryService
    {
        private PageCategoryRepository _pageCategoryRepo;
        private TransactionManager _transactionManager;
        private readonly PageCategoryMaker _pageCategoryMaker;

        public PageCategoryServiceImpl(PageCategoryRepository pageCategoryRepo, TransactionManager transactionManager, PageCategoryMaker pageCategoryMaker)
        {
            _pageCategoryRepo = pageCategoryRepo;
            _transactionManager = transactionManager;
            _pageCategoryMaker = pageCategoryMaker;
        }

        public void delete(long page_category_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var pageCategory = _pageCategoryRepo.getById(page_category_id);
                if (pageCategory == null)
                {
                    throw new ItemNotFoundException($"Page Category with id {page_category_id} doesn't exist.");
                }

                if (pageCategory.hasPages())
                {
                    throw new ItemUsedException("Page Category has  been already assigned to some pages.");
                }

                _pageCategoryRepo.delete(pageCategory);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void save(PageCategoryDto pageCategoryDto)
        {
            try
            {
                _transactionManager.beginTransaction();
                var pageCategoryWithSameName = _pageCategoryRepo.getByName(pageCategoryDto.name);
                if (pageCategoryWithSameName != null)
                {
                    throw new DuplicateItemException("Page category with same name already exist.");
                }

                PageCategory page_category = new PageCategory();
                _pageCategoryMaker.copy(ref page_category, pageCategoryDto);
                _pageCategoryRepo.insert(page_category);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void update(PageCategoryDto pageCategoryDto)
        {
            try
            {
                _transactionManager.beginTransaction();

                var pageCategoryId = _pageCategoryRepo.getById(pageCategoryDto.page_category_id);

                if (pageCategoryId == null)
                {
                    throw new ItemNotFoundException($"Page Category with id {pageCategoryDto.page_category_id} doesnot exist.");
                }

                var pageCategoryWithSameName = _pageCategoryRepo.getByName(pageCategoryDto.name);

                bool isNameAllowed = pageCategoryWithSameName == null || pageCategoryWithSameName.page_category_id == pageCategoryDto.page_category_id;
                if (!isNameAllowed)
                {
                    throw new DuplicateItemException("Page category with same name already exist.");
                }

                _pageCategoryMaker.copy(ref pageCategoryId, pageCategoryDto);
                _pageCategoryRepo.update(pageCategoryId);

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void enable(long page_category_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var pageCategory = _pageCategoryRepo.getById(page_category_id);
                if (pageCategory == null)
                {
                    throw new ItemNotFoundException($"Page Category with id {page_category_id} doesnot exist.");
                }

                pageCategory.enable();
                _pageCategoryRepo.update(pageCategory);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void disable(long page_category_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var pageCategory = _pageCategoryRepo.getById(page_category_id);
                if (pageCategory == null)
                {
                    throw new ItemNotFoundException($"Page Category with id {page_category_id} doesnot exist.");
                }

                pageCategory.disable();
                _pageCategoryRepo.update(pageCategory);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }
    }
}
