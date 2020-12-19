using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Repository.Interface;
using CMS.Web.Areas.Admin.FilterModel;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers
{
    [Route("event")]
    public class EventController : Controller
    {

        private readonly EventRepository _eventRepo;
        private readonly NoticeRepository _noticeRepo;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly SetupRepository _setupRepo;
        private readonly CareerRepository _careerRepo;
        private readonly TestimonialRepository _testimonialRepo;
        public EventController(EventRepository eventRepo, NoticeRepository noticeRepo, PaginatedMetaService paginatedMetaService, SetupRepository setupRepo, CareerRepository careerRepo, TestimonialRepository testimonialRepo)
        {
            _eventRepo = eventRepo;
            _noticeRepo = noticeRepo;
            _paginatedMetaService = paginatedMetaService;
            _setupRepo = setupRepo;
            _careerRepo = careerRepo;
            _testimonialRepo = testimonialRepo;


        }
        [Route("")]
        [Route("index")]
        public IActionResult Index(EventFilter filter = null)
        {
            var notice = _noticeRepo.getQueryable().Where(n => n.notice_expiry_date >= DateTime.Now && n.is_closed == false).Take(3).ToList();
            ViewBag.notices = notice;

            var testimonialValues = _testimonialRepo.getQueryable().ToList();
            ViewBag.testimonial = testimonialValues;
            var events = _eventRepo.getQueryable();
            ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(events.Count(), filter.page, 5);
            events = events.Skip(filter.number_of_rows * (filter.page - 1)).Take(5);
            ViewBag.events = events.ToList().OrderByDescending(a => a.event_to_date);

            return View(events.OrderByDescending(a => a.event_to_date).ToList());

        }

        [HttpGet]
        [Route("detail/{slug}")]
        public IActionResult detail(string slug)
        {
            var setupValues = _setupRepo.getQueryable().ToList();
            ViewBag.setup = setupValues;


            var career = _careerRepo.getQueryable().Where(n => n.closing_date >= DateTime.Now && n.is_closed == false).Take(4).ToList();
            ViewBag.careers = career;
            var eventDetail = _eventRepo.getBySlug(slug);
            return View(eventDetail);
        }
    }
}
