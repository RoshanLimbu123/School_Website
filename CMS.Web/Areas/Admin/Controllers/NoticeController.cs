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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Areas.Core.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/notice")]
    public class NoticeController : Controller
    {
        private readonly NoticeRepository _noticeRepo;
        private readonly NoticeService _noticeService;
        private readonly IMapper _mapper;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly FileHelper _fileHelper;

        public NoticeController(NoticeRepository noticeRepo, NoticeService noticeService, IMapper mapper, PaginatedMetaService paginatedMetaService,FileHelper fileHelper)
        {
            _noticeRepo = noticeRepo;
            _noticeService = noticeService;
            _mapper = mapper;
            _paginatedMetaService = paginatedMetaService;
            _fileHelper = fileHelper;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index(NoticeFilter filter = null)
        {
            try
            {
                var notices = _noticeRepo.getQueryable();

                if (!string.IsNullOrWhiteSpace(filter.title))
                {
                    notices = notices.Where(a => a.title.Contains(filter.title));
                }

                notices = notices.Where(a => a.notice_date >= filter.from_date && a.notice_date <= filter.to_date);

                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(notices.Count(), filter.page, filter.number_of_rows);


                notices = notices.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);

                var noticeDetails = notices.ToList();

                return View(noticeDetails);
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
        public IActionResult add(NoticeDto notice_dto,IFormFile file=null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string fileName = notice_dto.title;
                        notice_dto.image_name = _fileHelper.saveImageAndGetFileName(file, fileName);

                    }
                    _noticeService.save(notice_dto);
                    AlertHelper.setMessage(this, "Notice saved successfully.", messageType.success);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(notice_dto);
        }

        [HttpGet]
        [Route("edit/{notice_id}")]
        public IActionResult edit(long notice_id)
        {
            try
            {
                var notice = _noticeRepo.getById(notice_id);
                NoticeDto dto = _mapper.Map<NoticeDto>(notice);

                RouteData.Values.Remove("notice_id");
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
        public IActionResult edit(NoticeDto notice_dto,IFormFile file=null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string fileName = notice_dto.title;
                        notice_dto.image_name = _fileHelper.saveImageAndGetFileName(file, fileName);

                    }
                    _noticeService.update(notice_dto);
                    AlertHelper.setMessage(this, "Notice updated successfully.");
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(notice_dto);
        }

        [HttpGet]
        [Route("close/{notice_id}")]
        public IActionResult close(long notice_id)
        {
            try
            {
                _noticeService.close(notice_id);
                AlertHelper.setMessage(this, "Notice closed successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("unclose/{notice_id}")]
        public IActionResult disable(long notice_id)
        {
            try
            {
                _noticeService.unclose(notice_id);
                AlertHelper.setMessage(this, "Notice unclosed successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("delete/{notice_id}")]
        public IActionResult delete(long notice_id)
        {
            try
            {
                _noticeService.delete(notice_id);
                AlertHelper.setMessage(this, "Notice deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}