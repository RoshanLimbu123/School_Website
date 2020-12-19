using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Repository.Interface;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers
{
    [Route("franchise-model")]
    public class FranchiseModelController : Controller
    {
        private readonly FranchiseModelRepository _franchiseModelRepository;
        private readonly PaginatedMetaService _paginatedMetaService;

        public FranchiseModelController(FranchiseModelRepository franchiseModelRepository, PaginatedMetaService paginatedMetaService)
        {
            _franchiseModelRepository = franchiseModelRepository;
            _paginatedMetaService = paginatedMetaService;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var franchise = _franchiseModelRepository.getQueryable().OrderBy(a => a.is_enabled == true).ToList();
            ViewBag.franchisemodel = franchise;
            return View(franchise);
        }
        [HttpGet]
        [Route("detail/{slug}")]
        public IActionResult detail(string slug)
        {
            var franchiseDetail = _franchiseModelRepository.getBySlug(slug);
            return View(franchiseDetail);
        }
    }
}
