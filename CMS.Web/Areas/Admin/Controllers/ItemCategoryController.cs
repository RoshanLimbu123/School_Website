using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Core.FilterModel;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Areas.Core.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/item-category")]
    public class ItemCategoryController : Controller
    {
        private readonly ItemCategoryRepository _itemCategoryRepo;
        private readonly ItemCategoryService _itemCategoryService;
        private readonly IMapper _mapper;
        private readonly PaginatedMetaService _paginatedMetaService;

        public ItemCategoryController(ItemCategoryRepository itemCategoryRepo, ItemCategoryService itemCategoryService, IMapper mapper,PaginatedMetaService paginatedMetaService)
        {
            _itemCategoryRepo = itemCategoryRepo;
            _itemCategoryService = itemCategoryService;
            _mapper = mapper;
            _paginatedMetaService = paginatedMetaService;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index(ItemCategoryFilter filter=null)
        {
            try
            {
                var categories = _itemCategoryRepo.getQueryable();

                if (!string.IsNullOrWhiteSpace(filter.name))
                {
                    categories = categories.Where(a => a.name.Contains(filter.name));
                }

                if (filter.status == Enums.StatusFilter.Active)
                {
                    categories = categories.Where(a => a.is_enabled == true);
                }

                else if (filter.status == Enums.StatusFilter.Inactive)
                {
                    categories = categories.Where(a => a.is_enabled == false);
                }
                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(categories.Count(), filter.page, filter.number_of_rows);


                categories = categories.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);

                var itemCategories = categories.ToList();

                return View(itemCategories);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return Redirect("/admin");
            }
        }

        [Route("new")]
        public IActionResult add()
        {
            return View();
        }

        [HttpPost]
        [Route("new")]
        public IActionResult add(ItemCategoryDto item_category_dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _itemCategoryService.save(item_category_dto);
                    AlertHelper.setMessage(this, "Item Category saved successfully.", messageType.success);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(item_category_dto);
        }

        [HttpGet]
        [Route("edit/{item_category_id}")]
        public IActionResult edit(long item_category_id)
        {
            try
            {
                var item_category = _itemCategoryRepo.getById(item_category_id);
                ItemCategoryDto dto = _mapper.Map<ItemCategoryDto>(item_category);

                RouteData.Values.Remove("item_category_id");
                return View(dto);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult edit(ItemCategoryDto item_category_dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _itemCategoryService.update(item_category_dto);
                    AlertHelper.setMessage(this, "Item category updated successfully.");
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(item_category_dto);
        }

        [HttpGet]
        [Route("enable/{item_category_id}")]
        public IActionResult enable(long item_category_id)
        {
            try
            {
                _itemCategoryService.enable(item_category_id);
                AlertHelper.setMessage(this, "Item Category enabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("disable/{item_category_id}")]
        public IActionResult disable(long item_category_id)
        {
            try
            {
                _itemCategoryService.disable(item_category_id);
                AlertHelper.setMessage(this, "Item Category disabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("delete/{item_category_id}")]
        public IActionResult delete(long item_category_id)
        {
            try
            {
                _itemCategoryService.delete(item_category_id);
                AlertHelper.setMessage(this, "Item Category deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}