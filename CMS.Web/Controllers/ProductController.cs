using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Repository.Interface;
using CMS.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        private readonly ItemCategoryRepository _itemCategoryRepo;
        private readonly ProductRepository _productRepo;
        private readonly IMapper _mapper;

        public ProductController(ItemCategoryRepository itemCategoryRepo, ProductRepository productRepo, IMapper mapper)
        {
            _itemCategoryRepo = itemCategoryRepo;
            _productRepo = productRepo;
            _mapper = mapper;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var products = _productRepo.getAll();
            if (products == null)
            {
                return View(new ProductDetail());
            }
            ProductCategoryViewModel vm = new ProductCategoryViewModel();
            vm.products = new List<ProductDetail>();
            var productsList = new ProductDetail();
           
            foreach(var product in products)
            {
               var data=_mapper.Map<ProductDetail>(product);
                vm.products.Add(data);
            }
            return View(vm);
        }
        [HttpGet]
        [Route("detail/{slug}")]
        public IActionResult detail(string slug)
        {
            var productDetails = _productRepo.getBySlug(slug);
            if (productDetails == null)
            {
                return View(new ProductDetail());
            }
            var data = _mapper.Map<ProductDetail>(productDetails);

            return View(data);
        }

    }
}