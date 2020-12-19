using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Admin.FilterModel;
using CMS.Web.Areas.Core.Models;
using CMS.Web.Areas.Core.ViewModels;
using CMS.Web.Controllers;
using CMS.Web.Exceptions;
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
    [Route("admin/gallery-image")]
    public class GalleryImageController : BaseController
    {
        private GalleryImageService _galleryImageService;
        private GalleryImageRepository _galleryImageRepo;
        private GalleryRepository _galleryRepository;
        private readonly PaginatedMetaService _paginatedMetaService;
        private IMapper _mapper;
        private FileHelper _fileHelper;
        public GalleryImageController(FileHelper fileHelper, IMapper mapper, GalleryImageService galleryImageService, GalleryImageRepository galleryImageRepo, PaginatedMetaService paginatedMetaService, GalleryRepository galleryRepository)
        {
            _galleryImageService = galleryImageService;
            _galleryImageRepo = galleryImageRepo;
            _paginatedMetaService = paginatedMetaService;
            _mapper = mapper;
            _fileHelper = fileHelper;
            _galleryRepository = galleryRepository;
        }

        [Route("")]
        public IActionResult Index(GalleryImageFilter filter = null)
        {
            try
            {
                var galleries = _galleryImageRepo.getQueryable();
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
           
            
                ViewBag.image = new SelectList(_galleryRepository.getAll(), "gallery_id", "name");
                return View();

        }
        [HttpPost]
        [Route("new")]
        public IActionResult add(GalleryImageDto model, IFormFile file)
        {
            try
            {
                if (file == null)
                {
                    throw new CustomException("Image is required.");
                }
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        model.image_name = _fileHelper.saveImageAndGetFileName(file, model.title);
                    }
                    _galleryImageService.save(model);

                    AlertHelper.setMessage(this, "Gallery Image saved successfully.", messageType.success);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }

            ViewBag.image = new SelectList(_galleryRepository.getAll(), "gallery_id", "name");
            return View(model);
        }

        [HttpGet]
        [Route("edit/{gallery_image_id}")]
        public IActionResult edit(long gallery_image_id)
        {
            try
            {
                ViewBag.image = new SelectList(_galleryRepository.getAll(), "gallery_id", "name");

                CMS.Core.Entity.GalleryImage gallery = _galleryImageRepo.getById(gallery_image_id);
                GalleryImageModel galleryImageModel = _mapper.Map<GalleryImageModel>(gallery);
                return View(galleryImageModel);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }

        }

        [HttpPost]
        [Route("edit")]
        public IActionResult edit(GalleryImageModel model, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GalleryImageDto galleryImageDto = new GalleryImageDto();
                    galleryImageDto.gallery_image_id = model.gallery_image_id;
                    galleryImageDto.gallery_id= model.gallery_id;
                    galleryImageDto.title = model.title;
                    galleryImageDto.description = model.description;
                    galleryImageDto.is_slider_image = model.is_slider_image;
                    galleryImageDto.is_enabled = model.is_enabled;
                    galleryImageDto.is_default = model.is_default;
                    if (file != null)
                    {
                        galleryImageDto.image_name = _fileHelper.saveImageAndGetFileName(file, model.title);
                    }

                 
                    _galleryImageService.update(galleryImageDto);
                    AlertHelper.setMessage(this, "Gallery Image updated successfully.", messageType.success);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            ViewBag.image = new SelectList(_galleryRepository.getAll(), "gallery_id", "name");

            return View(model);
        }

        [HttpGet]
        [Route("enable/{gallery_image_id}")]
        public IActionResult enable(long gallery_image_id)
        {
            try
            {
                _galleryImageService.enable(gallery_image_id);
                AlertHelper.setMessage(this, "Gallery Image enabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("disable/{gallery_image_id}")]
        public IActionResult default1(long gallery_image_id)
        {
            try
            {
                _galleryImageService.disable(gallery_image_id);
                AlertHelper.setMessage(this, "Gallery Image set to default successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }




        [HttpGet]
        [Route("defaults/{gallery_image_id}")]
        public IActionResult defaults(long gallery_image_id)
        {
            try
            {
                _galleryImageService.default1(gallery_image_id);
                AlertHelper.setMessage(this, "Gallery Image set to Default successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("customs/{gallery_image_id}")]
        public IActionResult customs(long gallery_image_id)
        {
            try
            {
                _galleryImageService.custom(gallery_image_id);
                AlertHelper.setMessage(this, "Gallery Image Customed successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("make-slider/{gallery_image_id}")]
        public IActionResult makeSlider(long gallery_image_id)
        {
            try
            {
                _galleryImageService.makeSliderImage(gallery_image_id);
                AlertHelper.setMessage(this, "Gallery Image set to Home image slider successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("unmake-slider/{gallery_image_id}")]
        public IActionResult unmakeSlider(long gallery_image_id)
        {
            try
            {
                _galleryImageService.removeFromSliderImage(gallery_image_id);
                AlertHelper.setMessage(this, "Gallery Image removed from Home slider image successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("delete/{gallery_image_id}")]
        public IActionResult delete(long gallery_image_id)
        {
            try
            {
                _galleryImageService.delete(gallery_image_id);
                AlertHelper.setMessage(this, "Gallery image deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        private object getViewModelFrom(List<CMS.Core.Entity.GalleryImage> galleries)
        {
            GalleryImageIndexViewModel vm = new GalleryImageIndexViewModel();
            vm.gellery_details = new List<GalleryImageDetailModel>();
            foreach (var gallery in galleries)
            {
                var galleryDetail = _mapper.Map<GalleryImageDetailModel>(gallery);
                vm.gellery_details.Add(galleryDetail);
            }

            return vm;
        }
    }
}