using AutoMapper;
using CMS.Core.Repository.Interface;
using CMS.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CMS.Web.Controllers
{
    [Route("page")]
    public class PageController :Controller
    {
        private readonly CareerRepository _careerRepo;
        private readonly PageRepository _pageRepo;
        private readonly SetupRepository _setupRepo;
        private IMapper _mapper;
        public PageController(PageRepository pageRepo,IMapper mapper, CareerRepository careerRepo, SetupRepository setupRepo)
        {
            _careerRepo = careerRepo;
            _pageRepo = pageRepo;
            _mapper = mapper;
            _setupRepo = setupRepo;
        }

        [HttpGet]
        [Route("{slug}")]
        public IActionResult Index(string slug)
        {
            var setupValues = _setupRepo.getQueryable().ToList();
            ViewBag.setup = setupValues;


            var career = _careerRepo.getQueryable().Where(n => n.closing_date >= DateTime.Now && n.is_closed == false).Take(4).ToList();
            ViewBag.careers = career;

            var page = _pageRepo.getBySlug(slug);
            if (page == null)
            {
                return View(new PageDetail());
            }
            var pageDetail = _mapper.Map<PageDetail>(page);
            return View(pageDetail);
        }

       
    }
}