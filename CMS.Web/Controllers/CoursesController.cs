using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Repository.Interface;
using CMS.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers
{
    [Route("course")]
    public class CoursesController : Controller
    {
        private readonly CoursesRepository _productRepo;
        private readonly IMapper _mapper;

        public CoursesController(CoursesRepository productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var products = _productRepo.getAll();
            if (products == null)
            {
                return View(new CoursesDetail());
            }
            CourseViewModel vm = new CourseViewModel();
            vm.courses = new List<CoursesDetail>();
            var productsList = new CoursesDetail();
           
            foreach(var product in products)
            {
               var data=_mapper.Map<CoursesDetail>(product);
                vm.courses.Add(data);
            }
            return View(vm);
        }
        [HttpGet]
        [Route("detail/{slug}")]
        public IActionResult detail(string slug)
        {
            var courseDetail = _productRepo.getBySlug(slug);
            if (courseDetail == null)
            {
                return View(new CoursesDetail());
            }
            var data = _mapper.Map<CoursesDetail>(courseDetail);

            return View(data);
        }

    }
}