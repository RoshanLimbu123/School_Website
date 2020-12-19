using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Repository.Interface;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers
{
    [Route("services")]
    public class ServicesController : Controller
    {
        private readonly ServicesRepository _servicesRepository;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly SetupRepository _setupRepository;

        public ServicesController(ServicesRepository servicesRepository, SetupRepository setupRepository, PaginatedMetaService paginatedMetaService)
        {
            _servicesRepository = servicesRepository;
            _paginatedMetaService = paginatedMetaService;
            _setupRepository = setupRepository;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var services = _servicesRepository.getQueryable().Where(e => e.is_active == true).ToList();
            return View(services);
        }

        [HttpGet]
        [Route("detail/{slug}")]
        public IActionResult detail(string slug)
        {
            var servicesDetail = _servicesRepository.getBySlug(slug);
            return View(servicesDetail);
        }
    }
}