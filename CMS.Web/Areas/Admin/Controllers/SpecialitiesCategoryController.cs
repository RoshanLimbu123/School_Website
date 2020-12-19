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
using CMS.Web.Areas.Core.Models;
using CMS.Web.Areas.Core.ViewModels;
using CMS.Web.Controllers;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Areas.Core.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/specialities-category")]
    public class SpecialitiesCategoryController : BaseController
    {
        private SpecialitiesCategoryService _specialitiesCategoryService;
        private SpecialitiesCategoryRepository _specialitiesCategoryRepository;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly IMapper _mapper;
        private FileHelper _fileHelper;

        public SpecialitiesCategoryController(FileHelper fileHelper, IMapper mapper, SpecialitiesCategoryService specialitiesCategoryService, SpecialitiesCategoryRepository specialitiesCategoryRepository, PaginatedMetaService paginatedMetaService)
        {
            _specialitiesCategoryService = specialitiesCategoryService;
            _specialitiesCategoryRepository = specialitiesCategoryRepository;
            _paginatedMetaService = paginatedMetaService;
            _mapper = mapper;
            _fileHelper = fileHelper;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index(SpecialitiesCategoryFilter filter = null)
        {
            try
            {
                var pageCategories = _specialitiesCategoryRepository.getQueryable();
                if (!string.IsNullOrWhiteSpace(filter.title))
                {
                    pageCategories = pageCategories.Where(a => a.name.Contains(filter.title));
                }

                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(pageCategories.Count(), filter.page, filter.number_of_rows);

                pageCategories = pageCategories.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);
                var pageCat = pageCategories.ToList();

                var specialitiesCategoriesIndexVM = getViewModelFrom(pageCat);
                return View(specialitiesCategoriesIndexVM);
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
                SpecialitiesCategoryModel specialitiesCategoryModel = new SpecialitiesCategoryModel();
                return View(specialitiesCategoryModel);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        [Route("new")]
        public IActionResult add(SpecialitiesCategoryModel model, [FromForm(Name = "file")] IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SpecialitiesCategoryDto specialitiesCategoryDto = new SpecialitiesCategoryDto();

                       specialitiesCategoryDto.name = model.name;
                    if (file != null)
                    {
                        specialitiesCategoryDto.image_name = _fileHelper.saveImageAndGetFileName(file, model.name);
                    }
                    specialitiesCategoryDto.description = model.description;
                    specialitiesCategoryDto.is_enabled = model.is_enabled;

                    _specialitiesCategoryService.save(specialitiesCategoryDto);
                    AlertHelper.setMessage(this, "Specialities Category saved successfully.", messageType.success);
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
        [Route("edit/{specialities_category_id}")]
        public IActionResult edit(long specialities_category_id)
        {
            try
            {
                SpecialitiesCategory pageCategory = _specialitiesCategoryRepository.getById(specialities_category_id);
                SpecialitiesCategoryModel specialitiesCategoryModel = _mapper.Map<SpecialitiesCategoryModel>(pageCategory);
                return View(specialitiesCategoryModel);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }

        }

        [HttpPost]
        [Route("edit")]
        public IActionResult edit(SpecialitiesCategoryModel model, [FromForm(Name = "file")] IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SpecialitiesCategoryDto specialitiesCategoryDto = new SpecialitiesCategoryDto();
                    specialitiesCategoryDto.specialities_category_id = model.specialities_category_id;
                    specialitiesCategoryDto.name = model.name;
                    specialitiesCategoryDto.description = model.description;
                         if (file != null)
                    {
                        specialitiesCategoryDto.image_name = _fileHelper.saveImageAndGetFileName(file, model.name);
                    }
                
                    _specialitiesCategoryService.update(specialitiesCategoryDto);
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
        [Route("enable/{specialities_category_id}")]
        public IActionResult enable(long specialities_category_id)
        {
            try
            {
                _specialitiesCategoryService.enable(specialities_category_id);
                AlertHelper.setMessage(this, "Specialities Category enabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("disable/{specialities_category_id}")]
        public IActionResult disable(long specialities_category_id)
        {
            try
            {
                _specialitiesCategoryService.disable(specialities_category_id);
                AlertHelper.setMessage(this, "Specialities Category disabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("delete/{specialities_category_id}")]
        public IActionResult delete(long specialities_category_id)
        {
            try
            {
                _specialitiesCategoryService.delete(specialities_category_id);
                AlertHelper.setMessage(this, "Specialities Category deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        private SpecialitiesCategoryIndexViewModel getViewModelFrom(List<SpecialitiesCategory> specialitiesCategories)
        {
            SpecialitiesCategoryIndexViewModel vm = new SpecialitiesCategoryIndexViewModel();
            vm.specialitiesCategoryDetailModels = new List<specialitiesCategoryDetailModel>();
            foreach (var specialitiesCategory in specialitiesCategories)
            {
                var specialitiesCategoryDetail = _mapper.Map<specialitiesCategoryDetailModel>(specialitiesCategory);
                vm.specialitiesCategoryDetailModels.Add(specialitiesCategoryDetail);
            }
            return vm;
        }
    }
}