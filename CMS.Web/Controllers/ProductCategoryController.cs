using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Repository.Interface;
using CMS.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers
{
    [Route("product-category")]
    public class ProductCategoryController : Controller
    {
        private readonly ItemCategoryRepository _itemCategoryRepo;
        private readonly ProductRepository _productRepo;
        private readonly IMapper _mapper;

        public ProductCategoryController(ItemCategoryRepository itemCategoryRepo, ProductRepository productRepo, IMapper mapper)
        {
            _itemCategoryRepo = itemCategoryRepo;
            _productRepo = productRepo;
            _mapper = mapper;
        }

        [Route("{slug}")]
        public IActionResult Index(string slug)
        {
            ProductCategoryViewModel vm = new ProductCategoryViewModel();
            vm.products = new List<ProductDetail>();

            var itemCategory = _itemCategoryRepo.getBySlug(slug);
            if (itemCategory == null)
            {
                vm.category_detail = new ItemCategoryDto();
                return View(vm);
            }

            var products = itemCategory.products;
            foreach (var product in products)
            {
                vm.products.Add(_mapper.Map<ProductDetail>(product));
            }

            vm.category_detail = _mapper.Map<ItemCategoryDto>(itemCategory);

            return View(vm);
        }
    }
}