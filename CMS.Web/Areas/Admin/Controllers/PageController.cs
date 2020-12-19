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
    [Route("admin/page")]
    public class PageController : BaseController
    {
        private PageService _pageService;
        private PageRepository _pageRepo;
        private PageCategoryRepository _pageCategoryRepo;
        private readonly PaginatedMetaService _paginatedMetaService;
        private IMapper _mapper;
        private FileHelper _fileHelper;
        public PageController(PageCategoryRepository pageCategoryRepo, FileHelper fileHelper, IMapper mapper, PageService pageService, PageRepository pageRepo, PaginatedMetaService paginatedMetaService)
        {
            _pageService = pageService;
            _pageRepo = pageRepo;
            _paginatedMetaService = paginatedMetaService;
            _mapper = mapper;
            _fileHelper = fileHelper;
            _pageCategoryRepo = pageCategoryRepo;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index(PageFilter filter = null)
        {
            try
            {
                var pages = _pageRepo.getQueryable();
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
                PageModel pageModel = new PageModel();
                var pageCategories = _pageCategoryRepo.getQueryable().ToList();
                ViewBag.categories = new SelectList(pageCategories, "page_category_id", "name");
                return View(pageModel);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        [Route("new")]
        public IActionResult add(PageModel model, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PageDto pageDto = new PageDto();
                    pageDto.title = model.title;
                    if (file != null)
                    {
                        pageDto.image_name = _fileHelper.saveImageAndGetFileName(file, model.title);
                    }

                    pageDto.description = model.description;
                    pageDto.page_category_id = model.page_category_id;
                    pageDto.is_enabled = model.is_enabled;

                    _pageService.save(pageDto);
                    AlertHelper.setMessage(this, "Page saved successfully.", messageType.success);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            var pageCategories = _pageCategoryRepo.getQueryable().ToList();
            ViewBag.categories = new SelectList(pageCategories, "page_category_id", "name");
            return View(model);
        }

        [HttpGet]
        [Route("edit/{page_id}")]
        public IActionResult edit(long page_id)
        {
            try
            {
                var pageCategories = _pageCategoryRepo.getQueryable().ToList();
                ViewBag.categories = new SelectList(pageCategories, "page_category_id", "name");
                CMS.Core.Entity.Page page = _pageRepo.getById(page_id);
                PageModel pageModel = _mapper.Map<PageModel>(page);
                return View(pageModel);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }

        }

        [HttpPost]
        [Route("edit")]
        public IActionResult edit(PageModel model, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PageDto pageDto = new PageDto();
                    pageDto.page_id = model.page_id;
                    pageDto.title = model.title;
                    if (file != null)
                    {
                        pageDto.image_name = _fileHelper.saveImageAndGetFileName(file, model.title);
                    }

                    pageDto.description = model.description;
                    pageDto.page_category_id = model.page_category_id;
                    pageDto.is_enabled = model.is_enabled;
                    
                    _pageService.update(pageDto);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }

            var pageCategories = _pageCategoryRepo.getQueryable().ToList();
            ViewBag.categories = new SelectList(pageCategories, "page_category_id", "name");
            return View(model);
        }

        [HttpGet]
        [Route("make-home-page/{page_id}")]
        public IActionResult makeHomePage(long page_id)
        {
            try
            {
                _pageService.makeHomePage(page_id);
                AlertHelper.setMessage(this, "Home Page made successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("enable/{page_id}")]
        public IActionResult enable(long page_id)
        {
            try
            {
                _pageService.enable(page_id);
                AlertHelper.setMessage(this, "Page enabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("disable/{page_id}")]
        public IActionResult disable(long page_id)
        {
            try
            {
                _pageService.disable(page_id);
                AlertHelper.setMessage(this, "Page disabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("delete/{page_id}")]
        public IActionResult delete(long page_id)
        {
            try
            {
                _pageService.delete(page_id);
                AlertHelper.setMessage(this, "Page deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        private object getViewModelFrom(List<CMS.Core.Entity.Page> pages)
        {
            PageIndexViewModel vm = new PageIndexViewModel();
            vm.page_details = new List<PageDetailModel>();
            foreach (var page in pages)
            {
                var pageDetail = _mapper.Map<PageDetailModel>(page);
                pageDetail.page_category_name = page.page_category.name;
                vm.page_details.Add(pageDetail);
            }

            return vm;
        }
    }
}