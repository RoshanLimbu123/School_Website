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
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Areas.Core.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/page-category")]
    public class PageCategoryController : BaseController
    {
        private PageCategoryService _pageCategoryService;
        private PageCategoryRepository _pageCategoryRepo;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly IMapper _mapper;
        public PageCategoryController(IMapper mapper, PageCategoryService pageCategoryService, PageCategoryRepository pageCategoryRepo, PaginatedMetaService paginatedMetaService)
        {
            _pageCategoryService = pageCategoryService;
            _pageCategoryRepo = pageCategoryRepo;
            _paginatedMetaService = paginatedMetaService;
            _mapper = mapper;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index(PageCategoryFilter filter = null)
        {
            try
            {
                var pageCategories = _pageCategoryRepo.getQueryable();
                if (!string.IsNullOrWhiteSpace(filter.title))
                {
                    pageCategories = pageCategories.Where(a => a.name.Contains(filter.title));
                }

                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(pageCategories.Count(), filter.page, filter.number_of_rows);

                pageCategories = pageCategories.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);
                var pageCat = pageCategories.ToList();

                var pageCategoriesIndexVM = getViewModelFrom(pageCat);
                return View(pageCategoriesIndexVM);
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
                PageCategoryModel pageCategoryModel = new PageCategoryModel();
                return View(pageCategoryModel);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        [Route("new")]
        public IActionResult add(PageCategoryModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PageCategoryDto pageCategoryDto = new PageCategoryDto()
                    {
                        name = model.name,
                        is_enabled = model.is_enabled,
                    };
                    _pageCategoryService.save(pageCategoryDto);
                    AlertHelper.setMessage(this, "Page Category saved successfully.", messageType.success);
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
        [Route("edit/{page_category_id}")]
        public IActionResult edit(long page_category_id)
        {
            try
            {
                PageCategory pageCategory = _pageCategoryRepo.getById(page_category_id);
                PageCategoryModel pageCategoryModel = _mapper.Map<PageCategoryModel>(pageCategory);
                return View(pageCategoryModel);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }

        }

        [HttpPost]
        [Route("edit")]
        public IActionResult edit(PageCategoryModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PageCategoryDto pageCategoryDto = new PageCategoryDto()
                    {
                        page_category_id = model.page_category_id,
                        name = model.name,
                    };
                    _pageCategoryService.update(pageCategoryDto);
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
        [Route("enable/{page_category_id}")]
        public IActionResult enable(long page_category_id)
        {
            try
            {
                _pageCategoryService.enable(page_category_id);
                AlertHelper.setMessage(this, "Page Category enabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("disable/{page_category_id}")]
        public IActionResult disable(long page_category_id)
        {
            try
            {
                _pageCategoryService.disable(page_category_id);
                AlertHelper.setMessage(this, "Page Category disabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("delete/{page_category_id}")]
        public IActionResult delete(long page_category_id)
        {
            try
            {
                _pageCategoryService.delete(page_category_id);
                AlertHelper.setMessage(this, "Page Category deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        private PageCategoryIndexViewModel getViewModelFrom(List<PageCategory> pageCategories)
        {
            PageCategoryIndexViewModel vm = new PageCategoryIndexViewModel();
            vm.page_category_details = new List<PageCategoryDetailModel>();
            foreach (var pageCategory in pageCategories)
            {
                var pageCategoryDetail = _mapper.Map<PageCategoryDetailModel>(pageCategory);
                vm.page_category_details.Add(pageCategoryDetail);
            }
            return vm;
        }
    }
}