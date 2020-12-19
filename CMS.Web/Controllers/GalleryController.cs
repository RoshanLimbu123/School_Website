using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Entity;
using CMS.Core.Repository.Interface;
using CMS.Web.Areas.Core.ViewModels;
using CMS.Web.FilterModel;
using CMS.Web.LEPagination;
using CMS.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers
{
    
   
    [Route("gallery")]
    public class GalleryController : BaseController
    {
        private readonly GalleryRepository _galleryRepo;
        private readonly GalleryImageRepository _galleryImageRepo;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly IMapper _mapper;
        public GalleryController(GalleryRepository galleryRepo, PaginatedMetaService paginatedMetaService, GalleryImageRepository galleryImageRepo, IMapper mapper)
        {
            _galleryRepo = galleryRepo;
            _galleryImageRepo = galleryImageRepo;
            _paginatedMetaService = paginatedMetaService;
            _mapper = mapper;
        }

        public IActionResult Index(GalleryFilter filter = null)
        {
            var gallery = _galleryRepo.getQueryable().Where(a => a.is_active == true);

            ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(gallery.Count(), filter.page, filter.number_of_rows);


            gallery = gallery.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);

            GalleryViewModel model = getViewModel(gallery);

            return View(model);
        }



        [HttpGet]
        [Route("gallery-img/{gallery_id}")]
        public IActionResult galleryImg(long gallery_id)
        {
            var gallery = _galleryImageRepo.getQueryable().Where(a => a.gallery_id == gallery_id).ToList();
            GalleryImageViewModel vm = new GalleryImageViewModel();
            vm.gellery_details = new List<GalleryImageDetail>();
            foreach (var gal in gallery)
            {
                var detail = _mapper.Map<GalleryImageDetail>(gal);
                vm.gellery_details.Add(detail);
            }
            return View(vm);

        }


     
        private GalleryViewModel getViewModel(IQueryable<Core.Entity.Gallery> gallery)
        {
            GalleryViewModel vm = new GalleryViewModel();
            vm.gallery = new List<GalleryDetails>();
            foreach (var gall in gallery)
            {
                var detail = _mapper.Map<GalleryDetails>(gall);
                vm.gallery.Add(detail);
            }
            return vm;
        }
    }

    }
