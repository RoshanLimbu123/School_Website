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
    public class ItemCategoryServiceImpl : ItemCategoryService
    {
        private readonly TransactionManager _transactionManager;
        private readonly ItemCategoryRepository _itemCategoryRepo;
        private readonly ItemCategoryMaker _itemCategoryMaker;

        public ItemCategoryServiceImpl(TransactionManager transactionManager, ItemCategoryRepository itemCategoryRepo, ItemCategoryMaker itemCategoryMaker)
        {
            _transactionManager = transactionManager;
            _itemCategoryRepo = itemCategoryRepo;
            _itemCategoryMaker = itemCategoryMaker;
        }

        public void delete(long item_category_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var itemCategory = _itemCategoryRepo.getById(item_category_id);

                if (itemCategory == null)
                {
                    throw new ItemNotFoundException($"Item category with id {item_category_id} doesnot exist.");
                }

                if (itemCategory.hasProducts())
                {
                    throw new ChildCollectionsPresentException("Products have already been added to specified item category.");
                }
                _itemCategoryRepo.delete(itemCategory);

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }

        }

        public void disable(long item_category_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var itemCategory = _itemCategoryRepo.getById(item_category_id);

                if (itemCategory == null)
                {
                    throw new ItemNotFoundException($"Item category with id {item_category_id} doesnot exist.");
                }

                itemCategory.disable();
                _itemCategoryRepo.update(itemCategory);

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void enable(long item_category_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var itemCategory = _itemCategoryRepo.getById(item_category_id);

                if (itemCategory == null)
                {
                    throw new ItemNotFoundException($"Item category with id {item_category_id} doesnot exist.");
                }

                itemCategory.enable();
                _itemCategoryRepo.update(itemCategory);

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void save(ItemCategoryDto item_category_dto)
        {
            try
            {
                _transactionManager.beginTransaction();

                bool isNameValid = checkNameValidity(item_category_dto);

                if (!isNameValid)
                {
                    throw new DuplicateItemException("Item category with same name already exists.");
                }
                ItemCategory itemCategory = new ItemCategory();
                _itemCategoryMaker.copy(ref itemCategory, item_category_dto);

                _itemCategoryRepo.insert(itemCategory);

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void update(ItemCategoryDto item_category_dto)
        {
            try
            {
                _transactionManager.beginTransaction();

                bool isNameValid = checkNameValidity(item_category_dto);

                if (!isNameValid)
                {
                    throw new DuplicateItemException("Item category with same name already exists.");
                }
                ItemCategory itemCategory = _itemCategoryRepo.getById(item_category_dto.item_category_id);
                _itemCategoryMaker.copy(ref itemCategory, item_category_dto);

                _itemCategoryRepo.update(itemCategory);

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        private bool checkNameValidity(ItemCategoryDto item_category_dto)
        {
            var itemCategoryWithSameName = _itemCategoryRepo.getByName(item_category_dto.name);

            if (itemCategoryWithSameName == null || itemCategoryWithSameName.item_category_id == item_category_dto.item_category_id)
            {
                return true;
            }
            return false;

        }
    }
}
