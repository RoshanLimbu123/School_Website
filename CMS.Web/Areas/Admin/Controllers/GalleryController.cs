using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Exceptions;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Admin.FilterModel;
using CMS.Web.Areas.Admin.ViewModels;
using CMS.Web.Areas.Core.Models;
using CMS.Web.Areas.Core.ViewModels;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/gallery")]
    public class GalleryController : Controller
    {
        private readonly GalleryRepository _galleryRepository;
        private readonly GalleryService _galleryService;
        private readonly GalleryImageRepository _galleryImageRepository;
        private IMapper _mapper;
        private FileHelper _fileHelper;
        private readonly PaginatedMetaService _paginatedMetaService;

        public GalleryController(GalleryRepository galleryRepository, GalleryService galleryService, IMapper mapper, FileHelper fileHelper, PaginatedMetaService paginatedMetaService, GalleryImageRepository galleryImageRepository)
        {
            _galleryRepository = galleryRepository;
            _galleryService = galleryService;
            _mapper = mapper;
            _fileHelper = fileHelper;
            _paginatedMetaService = paginatedMetaService;
            _galleryImageRepository = galleryImageRepository;
        }

             [Route("")]
        public IActionResult Index(GalleryFilter filter = null)
        {
            try
            {
                var galleries = _galleryRepository.getQueryable();
              
                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(galleries.Count(), filter.page, filter.number_of_rows);
                galleries = galleries.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);
                var gallery = galleries.ToList();
                var galleryIndexVM = getViewModelFrom(gallery);
                return View(galleryIndexVM);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return Redirect("index");
            }
        }
        [HttpGet]
        [Route("new")]
        public IActionResult add()
        {
            try
            {
                GalleryModel galleryModel = new GalleryModel();
                return View(galleryModel);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [Route("new")]
        public IActionResult add(GalleryModel model)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                    GalleryDto galleryDto = new GalleryDto();
                    galleryDto.name = model.name;
                    galleryDto.description = model.description;
                    _galleryService.save(galleryDto);

                    AlertHelper.setMessage(this, "Gallery saved successfully.", messageType.success);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(model);
        }

        [HttpGet]
        [Route("edit/{gallery_id}")]
        public IActionResult edit(long gallery_id)
        {
            try
            {
                CMS.Core.Entity.Gallery gallery = _galleryRepository.getById(gallery_id);
                GalleryModel galleryModel = _mapper.Map<GalleryModel>(gallery);
                return View(galleryModel);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [Route("edit")]
        public IActionResult edit(GalleryModel model, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GalleryDto galleryDto = new GalleryDto();
                    galleryDto.gallery_id = model.gallery_id;
                    galleryDto.name =model.name;
                    galleryDto.description = model.description;

                    if (file != null)
                    {
                        galleryDto.name = _fileHelper.saveImageAndGetFileName(file, model.name);
                    }

                    galleryDto.is_active = model.is_active;

                    _galleryService.update(galleryDto);
                    AlertHelper.setMessage(this, "Gallery updated successfully.", messageType.success);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(model);
        }
        [HttpPost]
        [Route("ViewImg")]
        public IActionResult ViewImg()
        {
            return View();
        }
        [HttpGet]
        [Route("ViewImg/{gallery_id}")]
        public ActionResult ViewImg(long gallery_id)
        {
            var gallery = _galleryImageRepository.getQueryable().Where(a=>a.gallery_id==gallery_id).ToList();
            GalleryImageIndexViewModel vm = new GalleryImageIndexViewModel();
            vm.gellery_details = new List<GalleryImageDetailModel>();
            foreach (var gal in gallery)
            {
                var detail=_mapper.Map<GalleryImageDetailModel>(gal);
                vm.gellery_details.Add(detail);
            }
            return View(vm);
          
        }
        [HttpGet]
        [Route("active/{gallery_id}")]
        public IActionResult active(long gallery_id)
        {
            try
            {
                _galleryService.active(gallery_id);
                AlertHelper.setMessage(this, "Gallery activated successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("inactive/{gallery_id}")]
        public IActionResult inactive(long gallery_id)
        {
            try
            {
                _galleryService.inactive(gallery_id);
                AlertHelper.setMessage(this, "Gallery  inactivated successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Route("delete/{gallery_id}")]
        public IActionResult delete(long gallery_id)
        {
            try
            {
                _galleryService.delete(gallery_id);
                AlertHelper.setMessage(this, "Gallery deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        private object getViewModelFrom(List<CMS.Core.Entity.Gallery> galleries)
        {
            GalleryIndexViewModel gvm = new GalleryIndexViewModel();
            gvm.gallery_details = new List<GalleryDetailModel>();
            foreach (var gallery in galleries)
            {
                var galleryDetail = _mapper.Map<GalleryDetailModel>(gallery);
                gvm.gallery_details.Add(galleryDetail);
            }

            return gvm;
        }
    }
    }
