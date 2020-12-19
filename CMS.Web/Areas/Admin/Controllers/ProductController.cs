using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Core.FilterModel;
using CMS.Web.Areas.Core.ViewModels;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CMS.Web.Areas.Core.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/product")]
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepo;
        private readonly ProductService _productService;
        private readonly FileHelper _fileHelper;
        private readonly IMapper _mapper;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly ItemCategoryRepository _itemCategoryRepo;

        public ProductController(ProductRepository productRepo, ProductService productService, IMapper mapper, FileHelper fileHelper, PaginatedMetaService paginatedMetaService,ItemCategoryRepository itemCategoryRepo)
        {
            _productRepo = productRepo;
            _productService = productService;
            _mapper = mapper;
            _fileHelper = fileHelper;
            _paginatedMetaService = paginatedMetaService;
            _itemCategoryRepo = itemCategoryRepo;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index(ProductFilter filter = null)
        {
            try
            {
                var products = _productRepo.getQueryable();

                if (!string.IsNullOrWhiteSpace(filter.title))
                {
                    products = products.Where(a => a.name.Contains(filter.title));
                }

                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(products.Count(), filter.page, filter.number_of_rows);


                products = products.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);

                var productDetails = products.ToList();

                ProductIndexViewModel productIndexVM = getProductIndexVM(productDetails);

                return View(productIndexVM);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return Redirect("/admin");
            }
        }

        [Route("new")]
        public IActionResult add()
        {
            ViewBag.item_categories = new SelectList(_itemCategoryRepo.getAll(), "item_category_id", "name");
            return View();
        }

        [HttpPost]
        [Route("new")]
        public IActionResult add(ProductDto product_dto, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        product_dto.file_name = _fileHelper.saveImageAndGetFileName(file, product_dto.name);
                    }
                    _productService.save(product_dto);
                    AlertHelper.setMessage(this, "Product saved successfully.", messageType.success);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            ViewBag.item_categories = new SelectList(_itemCategoryRepo.getAll(), "item_category_id", "name");
            return View(product_dto);
        }

        [HttpGet]
        [Route("edit/{product_id}")]
        public IActionResult edit(long product_id)
        {
            try
            {
                ViewBag.item_categories = new SelectList(_itemCategoryRepo.getAll(), "item_category_id", "name");
                var product = _productRepo.getById(product_id);
                ProductDto dto = _mapper.Map<ProductDto>(product);

                RouteData.Values.Remove("product_id");
                return View(dto);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult edit(ProductDto product_dto, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string fileName = product_dto.name;
                        product_dto.file_name = _fileHelper.saveImageAndGetFileName(file, fileName);

                    }
                    _productService.update(product_dto);
                    AlertHelper.setMessage(this, "Product updated successfully.");
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            ViewBag.item_categories = new SelectList(_itemCategoryRepo.getAll(), "item_category_id", "name");
            return View(product_dto);
        }

        [HttpGet]
        [Route("enable/{product_id}")]
        public IActionResult enable(long product_id)
        {
            try
            {
                _productService.enable(product_id);
                AlertHelper.setMessage(this, "Product enabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("disable/{product_id}")]
        public IActionResult disable(long product_id)
        {
            try
            {
                _productService.disable(product_id);
                AlertHelper.setMessage(this, "Product disabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("delete/{product_id}")]
        public IActionResult delete(long product_id)
        {
            try
            {
                _productService.delete(product_id);
                AlertHelper.setMessage(this, "Product deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        private ProductIndexViewModel getProductIndexVM(List<Product> productDetails)
        {
            ProductIndexViewModel vm = new ProductIndexViewModel();
            vm.products = new List<ProductDetail>();

            foreach (var product in productDetails)
            {
                var convertedProduct = _mapper.Map<ProductDetail>(product);
                convertedProduct.item_category_name = product.item_category?.name;
                vm.products.Add(convertedProduct);
            }

            return vm;
        }
    }
}