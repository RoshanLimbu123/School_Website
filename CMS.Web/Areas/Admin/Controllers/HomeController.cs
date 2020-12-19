using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Repository.Interface;
using CMS.Web.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin")]
    public class HomeController : Controller
    {
        private readonly NoticeRepository _noticeRepo;
        private readonly CareerRepository _careerRepo;
        private readonly CoursesRepository _productRepo;
        private readonly PageRepository _pageRepo;
        private readonly SetupRepository _setupRepo;

        public HomeController(NoticeRepository noticeRepo, CoursesRepository productRepo, CareerRepository careerRepo, PageRepository pageRepo, SetupRepository setupRepo)
        {
            _noticeRepo = noticeRepo;
            _careerRepo = careerRepo;
            _productRepo = productRepo;
            _pageRepo = pageRepo;
            _setupRepo = setupRepo;
        }

        [Route("")]
        public IActionResult Index()
        {
            HomeIndexViewModel homeIndexVM = new HomeIndexViewModel();
            homeIndexVM.active_careers_count = _careerRepo.getQueryable().Where(a => a.is_closed == false && (a.closing_date == null ? true : (DateTime.Now <= a.closing_date))).Count();


            homeIndexVM.active_products_count = _productRepo.getQueryable().Where(a => a.is_enabled == true).Count();

            homeIndexVM.active_notices_count = _noticeRepo.getQueryable().Where(a => a.notice_expiry_date >= DateTime.Now).Count();

            homeIndexVM.pages_count = _pageRepo.getQueryable().Count();

            var setup = _setupRepo.getQueryable().Where(a => a.key == Models.SetupKeys.getOrganisationNameKey).SingleOrDefault();
            var address = _setupRepo.getQueryable().Where(a => a.key == Models.SetupKeys.getAddressKey).SingleOrDefault();

            homeIndexVM.company_name = setup?.value;
            homeIndexVM.address = address?.value;
            return View(homeIndexVM);
        }
    }
}