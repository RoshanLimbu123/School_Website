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
    public class ProductServiceImpl : ProductService
    {
        private readonly TransactionManager _transactionManager;
        private readonly ProductRepository _productRepo;
        private readonly ProductMaker _productMaker;
        private readonly ItemCategoryRepository _itemCategoryRepo;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProductServiceImpl(TransactionManager transactionManager, ProductRepository productRepo, ProductMaker productMaker, ItemCategoryRepository itemCategoryRepo, IHostingEnvironment hostingEnvironment)
        {
            _itemCategoryRepo = itemCategoryRepo;
            _transactionManager = transactionManager;
            _productRepo = productRepo;
            _productMaker = productMaker;
            _hostingEnvironment = hostingEnvironment;
        }

        public void delete(long product_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var product = _productRepo.getById(product_id);

                if (product == null)
                {
                    throw new ItemNotFoundException($"Product with id {product_id} doesnot exist.");
                }

                _productRepo.delete(product);

                deleteImage(product.file_name);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void disable(long product_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var product = _productRepo.getById(product_id);

                if (product == null)
                {
                    throw new ItemNotFoundException($"Product with id {product_id} doesnot exist.");
                }

                product.disable();
                _productRepo.update(product);

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void enable(long product_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var product = _productRepo.getById(product_id);

                if (product == null)
                {
                    throw new ItemNotFoundException($"Product with id {product_id} doesnot exist.");
                }

                product.enable();
                _productRepo.update(product);

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void save(ProductDto product_dto)
        {
            try
            {
                _transactionManager.beginTransaction();

                bool isNameValid = checkNameValidity(product_dto);

                if (!isNameValid)
                {
                    throw new DuplicateItemException("Product with same name already exists.");
                }
                Product product = new Product();
                _productMaker.copy(ref product, product_dto);
                if (product.item_category_id != 0)
                {
                    product.item_category = _itemCategoryRepo.getById(product.item_category_id) ?? throw new ItemNotFoundException($"Item category with the id {product.item_category_id} doesnot exist.");
                }

                _productRepo.insert(product);

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void update(ProductDto product_dto)
        {
            try
            {
                _transactionManager.beginTransaction();

                bool isNameValid = checkNameValidity(product_dto);

                if (!isNameValid)
                {
                    throw new DuplicateItemException("Product with same name already exists.");
                }
                Product product =_productRepo.getById(product_dto.product_id);

                string oldImage = product.file_name;


                _productMaker.copy(ref product, product_dto);
                if (product.item_category_id != 0)
                {
                    product.item_category = _itemCategoryRepo.getById(product.item_category_id) ?? throw new ItemNotFoundException($"Item category with the id {product.item_category_id} doesnot exist.");
                }
                _productRepo.update(product);

                if (!string.IsNullOrWhiteSpace(product_dto.file_name))
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

        private void deleteImage(string file_name)
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/custom");
            if (File.Exists(Path.Combine(filePath, file_name)))
            {
                File.Delete(Path.Combine(filePath, file_name));
            }
        }

        private bool checkNameValidity(ProductDto product_dto)
        {
            List<Product> productsWithSameName = _productRepo.getByName(product_dto.name);
            var productsWithSameNameInSameCategory = productsWithSameName.Where(a => a.item_category_id == product_dto.item_category_id).SingleOrDefault();

            if (productsWithSameNameInSameCategory == null || productsWithSameNameInSameCategory.product_id == product_dto.product_id)
            {
                return true;
            }
            return false;
        }
    }
}
