using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Helper;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CMS.Core.Service.Implementation
{
    public class PageServiceImpl : PageService
    {
        private PageRepository _pageRepo;
        private TransactionManager _transactionManager;
        private readonly PageMaker _pageMaker;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PageServiceImpl(IHostingEnvironment hostingEnvironment, PageRepository pageRepo, TransactionManager transactionManager, PageMaker pageMaker)
        {
            _pageRepo = pageRepo;
            _transactionManager = transactionManager;
            _pageMaker = pageMaker;
            _hostingEnvironment = hostingEnvironment;
        }

        public void delete(long page_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var page = _pageRepo.getById(page_id);
                if (page == null)
                {
                    throw new ItemNotFoundException($"Page Category with id {page_id} doesn't exist.");
                }
                string oldImage = page.image_name;

                _pageRepo.delete(page);
                if (!string.IsNullOrWhiteSpace(oldImage))
                {
                    deleteImage(oldImage);
                }

                _transactionManager.commitTransaction();

            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void makeHomePage(long page_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var hasHomePage = _pageRepo.getHomePage();

                if (hasHomePage != null)
                {
                    hasHomePage.unmakeHomePage();
                    _pageRepo.update(hasHomePage);
                }
                var page = _pageRepo.getById(page_id);
                if (page == null)
                    throw new ItemNotFoundException($"Page with id {page_id} doesnot exist.");
                page.makeHomePage();
                page.enable();
                _pageRepo.update(page);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }


        public void enable(long page_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var page = _pageRepo.getById(page_id);
                if (page == null)
                    throw new ItemNotFoundException($"Page with id {page_id} doesnot exist.");

                page.enable();
                _pageRepo.update(page);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void disable(long page_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var page = _pageRepo.getById(page_id);
                if (page == null)
                    throw new ItemNotFoundException($"Page with id {page_id} doesnot exist.");

                page.disable();
                _pageRepo.update(page);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void save(PageDto pageDto)
        {
            try
            {
                _transactionManager.beginTransaction();
                bool isNameValid = checkNameValidity(pageDto);
                if (!isNameValid)
                {
                    throw new DuplicateItemException("Page with same name already exist.");
                }
                Page page = new Page();
                _pageMaker.copy(ref page, pageDto);
                _pageRepo.insert(page);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void update(PageDto pageDto)
        {
            try
            {
                _transactionManager.beginTransaction();

                var page = _pageRepo.getById(pageDto.page_id);
                if (page == null)
                {
                    throw new ItemNotFoundException($"Page with id {pageDto.page_id} doesnot exist.");
                }

                bool isNameValid = checkNameValidity(pageDto);
                if (!isNameValid)
                {
                    throw new DuplicateItemException("Page with same name already exist.");
                }

                string oldImage = page.image_name;

                _pageMaker.copy(ref page, pageDto);
                _pageRepo.update(page);

                if (!string.IsNullOrWhiteSpace(pageDto.image_name))
                {
                    if (!string.IsNullOrWhiteSpace(oldImage))
                    {
                        deleteImage(oldImage);
                    }
                }

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        private bool checkNameValidity(PageDto pageDto)
        {
            List<Page> pageWithSameName = _pageRepo.getByName(pageDto.title.ToLower());
            var pageWithSameNameInSameCategory = pageWithSameName.Where(a => a.page_category_id == pageDto.page_category_id).SingleOrDefault();

            if (pageWithSameNameInSameCategory == null || pageWithSameNameInSameCategory.page_id == pageDto.page_id)
            {
                return true;
            }
            return false;
        }

        protected void deleteImage(string image_path)
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/custom");
            if (File.Exists(Path.Combine(filePath, image_path)))
            {
                File.Delete(Path.Combine(filePath, image_path));

            }
        }
    }
}
