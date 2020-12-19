using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Core.ViewModels;
using CMS.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CMS.Web.Models;
using CMS.Core.Entity;

namespace CMS.Web.ViewComponents
{
    [ViewComponent(Name = "HeaderView")]
    public class HeaderViewComponent : ViewComponent
    {
        public GalleryRepository _galleryRepo { get; set; }
        public NoticeRepository _noticeRepo { get; set; }
        private ServicesRepository _servicesRepository { get; set; }
        public SetupRepository _setupRepo { get; set; }
        public PageCategoryRepository _pageCategoryRepo { get; set; }
        public ProductRepository _productRepo { get; set; }
        public ItemCategoryRepository _itemCategoryRepo { get; set; }
        public CareerRepository _careerRepository { get; set; }
        public SpecialitiesCategoryRepository _specialitiesCategoryRepository { get; set; }
        private IMapper _mapper;
        public EnquiryRepository _enquiryRepository { get; set; }
        public EnquiryService _enquiryService { get; set; }


        public HeaderViewComponent(EnquiryService enquiryService, EnquiryRepository enquiryRepository, SpecialitiesCategoryRepository specialitiesCategoryRepository, CareerRepository careerRepository, IMapper mapper, ItemCategoryRepository itemCategoryRepository, ProductRepository productRepository, GalleryRepository galleryRepo, ServicesRepository servicesRepository, NoticeRepository noticeRepo, SetupRepository setupRepo, PageCategoryRepository pageCategoryRepo)
        {
            _galleryRepo = galleryRepo;
            _noticeRepo = noticeRepo;
            _setupRepo = setupRepo;
            _pageCategoryRepo = pageCategoryRepo;
            _servicesRepository = servicesRepository;
            _productRepo = productRepository;
            _itemCategoryRepo = itemCategoryRepository;
            _careerRepository = careerRepository;
            _specialitiesCategoryRepository = specialitiesCategoryRepository;
            _mapper = mapper;
            _enquiryRepository = enquiryRepository;
            _enquiryService = enquiryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
           


            var setupValues = _setupRepo.getQueryable().ToList();
            ViewBag.setup = setupValues;
            var serviceValue = _servicesRepository.getQueryable().Where(a => a.is_active == true).ToList();
            ViewBag.services = serviceValue;

            var pageCategory = _pageCategoryRepo.getQueryable().Where(a => a.is_enabled == true).ToList();
            ViewBag.pageCategories = pageCategory;

            var specialitiesCategory = _specialitiesCategoryRepository.getQueryable().Where(a => a.is_enabled == true).ToList();
            ViewBag.specialitiesCategories = specialitiesCategory;

            var productCat = _itemCategoryRepo.getQueryable().Where(a => a.is_enabled == true).ToList();
            ViewBag.productCategories = productCat;
            var products = _productRepo.getQueryable().Where(n => n.is_enabled == true).ToList();

            var careerValue = _careerRepository.getQueryable().Where(a => a.opening_date <= a.closing_date).ToList();
            ViewBag.career = careerValue;

            var careers = _careerRepository.getQueryable().Where(a => a.closing_date.Value > DateTime.Now.Date).ToList().Count;
            ViewBag.careerList = careers;

            List<ProductDetail> productDetails = new List<ProductDetail>();
            foreach (var product in products)
            {
                productDetails.Add(_mapper.Map<ProductDetail>(product));
            }

            ViewBag.products = productDetails;

           
            return View();
        }

       
     

    }
}
