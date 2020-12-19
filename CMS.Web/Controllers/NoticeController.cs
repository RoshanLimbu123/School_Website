using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Entity;
using CMS.Core.Repository.Interface;
using CMS.Web.Areas.Core.FilterModel;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers
{
    [Route("notice")]
    public class NoticeController : BaseController
    {
        private readonly NoticeRepository _noticeRepo;
        private readonly EventRepository _eventRepo;
        private readonly SetupRepository _setupRepo;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly CareerRepository _careerRepo;
        public NoticeController(NoticeRepository noticeRepo, EventRepository eventRepo, SetupRepository setupRepo, PaginatedMetaService paginatedMetaService, CareerRepository careerRepo)
        {
            _noticeRepo = noticeRepo;
            _eventRepo = eventRepo;
            _setupRepo = setupRepo;
            _paginatedMetaService = paginatedMetaService;
            _careerRepo = careerRepo;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index(NoticeFilter filter = null)
        {
            var eventValues = _eventRepo.getQueryable().Where(e => e.is_closed == false).Take(3).ToList();
            ViewBag.events = eventValues;
            var setupValues = _setupRepo.getQueryable().ToList();
            ViewBag.setup = setupValues;
            var recentNotice = _noticeRepo.recentNotice();
            ViewBag.recentNotices = recentNotice;
            var notices = _noticeRepo.getQueryable();
            ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(notices.Count(), filter.page, 10);
            notices = notices.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);
           // ViewBag.notices = notices.ToList();
            return View(notices.ToList());
        }

        [HttpGet]
        [Route("detail/{slug}")]
        public IActionResult detail(string slug)
        {
            var setupValues = _setupRepo.getQueryable().ToList();
            ViewBag.setup = setupValues;
            var career = _careerRepo.getQueryable().Where(n => n.closing_date >= DateTime.Now && n.is_closed == false).Take(4).ToList();
            ViewBag.careers = career;
            var noticeDetail = _noticeRepo.getBySlug(slug);
            return View(noticeDetail);
        }
    }
}