using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Core.FilterModel;
using CMS.Web.Areas.Core.Models;
using CMS.Web.Areas.Core.ViewModels;
using CMS.Web.Controllers;
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
    [Route("admin/specialities")]
    public class SpecialitiesController : BaseController
    {
        private SpecialitiesService _specialitiesService;
        private SpecialitiesRepository _specialitiesRepository;
        private SpecialitiesCategoryRepository _specialitiesCategoryRepository;
        private readonly PaginatedMetaService _paginatedMetaService;
        private IMapper _mapper;
        private FileHelper _fileHelper;
        public SpecialitiesController(SpecialitiesCategoryRepository specialitiesCategoryRepository, FileHelper fileHelper, IMapper mapper, SpecialitiesService specialitiesService, SpecialitiesRepository specialitiesRepository, PaginatedMetaService paginatedMetaService)
        {
            _specialitiesService = specialitiesService;
            _specialitiesRepository = specialitiesRepository;
            _paginatedMetaService = paginatedMetaService;
            _mapper = mapper;
            _fileHelper = fileHelper;
            _specialitiesCategoryRepository = specialitiesCategoryRepository;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index(SpecialitiesFilter filter = null)
        {
            try
            {
                var pages = _specialitiesRepository.getQueryable();
                if (!string.IsNullOrWhiteSpace(filter.title))
                {
                    pages = pages.Where(a => a.title.Contains(filter.title));
                }
                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(pages.Count(), filter.page, filter.number_of_rows);
                pages = pages.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);
                var page = pages.ToList();
                var pageIndexVM = getViewModelFrom(page);
                return View(pageIndexVM);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return Redirect("/admin");
            }
        }

        [HttpGet]
        [Route("new")]
        public IActionResult add()
        {
            try
            {
                SpecialitiesModel specialitiesModel  = new SpecialitiesModel();
                var pageCategories = _specialitiesCategoryRepository.getQueryable().ToList();
                ViewBag.specialitycategories = new SelectList(pageCategories, "specialities_category_id", "name");
                return View(specialitiesModel);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        [Route("new")]
        public IActionResult add(SpecialitiesModel model, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SpecialitiesDto specialitiesDto = new SpecialitiesDto();
                    specialitiesDto.title = model.title;
                    if (file != null)
                    {
                        specialitiesDto.image_name = _fileHelper.saveImageAndGetFileName(file, model.title);
                    }

                    specialitiesDto.description = model.description;
                    specialitiesDto.specialities_category_id = model.specialities_category_id;
                    specialitiesDto.is_enabled = model.is_enabled;

                    _specialitiesService.save(specialitiesDto);
                    AlertHelper.setMessage(this, "Specialities saved successfully.", messageType.success);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            var specialitiesCategories = _specialitiesCategoryRepository.getQueryable().ToList();
            ViewBag.specialitycategories = new SelectList(specialitiesCategories, "specialities_category_id", "name");
            return View(model);
        }

        [HttpGet]
        [Route("edit/{specialities_id}")]
        public IActionResult edit(long specialities_id)
        {
            try
            {
                var pageCategories = _specialitiesCategoryRepository.getQueryable().ToList();
                ViewBag.specialitycategories = new SelectList(pageCategories, "specialities_category_id", "name");
                CMS.Core.Entity.Specialities specialities = _specialitiesRepository.getById(specialities_id);
                SpecialitiesModel specialitiesModel = _mapper.Map<SpecialitiesModel>(specialities);
                return View(specialitiesModel);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }

        }

        [HttpPost]
        [Route("edit")]
        public IActionResult edit(SpecialitiesModel model, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SpecialitiesDto specialitiesDto = new SpecialitiesDto();
                    specialitiesDto.specialities_id = model.specialities_id;
                    specialitiesDto.title = model.title;
                    if (file != null)
                    {
                        specialitiesDto.image_name = _fileHelper.saveImageAndGetFileName(file, model.title);
                    }

                    specialitiesDto.description = model.description;
                    specialitiesDto.specialities_category_id = model.specialities_category_id;
                    specialitiesDto.is_enabled = model.is_enabled;
                    
                    _specialitiesService.update(specialitiesDto);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }

            var specialitiesCategories = _specialitiesCategoryRepository.getQueryable().ToList();
            ViewBag.specialitycategories = new SelectList(specialitiesCategories, "specialities_category_id", "name");
            return View(model);
        }

      

        [HttpGet]
        [Route("enable/{specialities_id}")]
        public IActionResult enable(long specialities_id)
        {
            try
            {
                _specialitiesService.enable(specialities_id);
                AlertHelper.setMessage(this, "Specialities enabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("disable/{specialities_id}")]
        public IActionResult disable(long specialities_id)
        {
            try
            {
                _specialitiesService.disable(specialities_id);
                AlertHelper.setMessage(this, "Specialities disabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("delete/{specialities_id}")]
        public IActionResult delete(long specialities_id)
        {
            try
            {
                _specialitiesService.delete(specialities_id);
                AlertHelper.setMessage(this, "Specialities deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        private object getViewModelFrom(List<CMS.Core.Entity.Specialities> specialities)
        {
            SpecialitiesIndexViewModel vm = new SpecialitiesIndexViewModel();
            vm.specialities_details = new List<SpecialitiesDetailModel>();
            foreach (var specialities1 in specialities)
            {
                var pageDetail = _mapper.Map<SpecialitiesDetailModel>(specialities1);
                pageDetail.specialities_category_name = specialities1.specialities_category.name;
                vm.specialities_details.Add(pageDetail);
            }

            return vm;
        }
    }
}