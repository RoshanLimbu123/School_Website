using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Repository.Interface;
using CMS.Web.Areas.Core.FilterModel;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers
{
    [Route("career")]
    public class CareerController : Controller
    {
        private readonly CareerRepository _careerRepo;
        private readonly SetupRepository _setupRepo;
        private readonly PaginatedMetaService _paginatedMetaService;

        public CareerController(CareerRepository careerRepo, SetupRepository setupRepo, PaginatedMetaService paginatedMetaService)
        {
            _careerRepo = careerRepo;
            _setupRepo = setupRepo;
            _paginatedMetaService = paginatedMetaService;
        }

        [Route("")]
        public IActionResult Index(CareerFilter filter = null)
        {
            var setupValues = _setupRepo.getQueryable().ToList();
            ViewBag.setup = setupValues;
            var careers = _careerRepo.getQueryable().Where(a => a.is_closed == false && (a.closing_date == null ? true : (DateTime.Now <= a.closing_date)));

            ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(careers.Count(), filter.page, 2);


            careers = careers.Skip(filter.number_of_rows * (filter.page - 1));
            ViewBag.careers = careers.ToList();
            return View(careers.ToList());

        }
        [HttpGet]
        [Route("detail/{slug}")]
        public IActionResult detail(string slug)
        {
            var setupValues = _setupRepo.getQueryable().ToList();
            ViewBag.setup = setupValues;
            var career = _careerRepo.getQueryable().Where(n => n.closing_date >= DateTime.Now && n.is_closed == false).Take(4).ToList();
            ViewBag.careers = career;
            var noticeDetail = _careerRepo.getBySlug(slug);
            return View(noticeDetail);
        }
    }
}