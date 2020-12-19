using AutoMapper;
using CMS.Core.Repository.Interface;
using CMS.Web.LEPagination;
using CMS.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Controllers
{
    [Route("specialities")]
    public class SpecialitiesController: Controller
    {
        private readonly SpecialitiesRepository _specialitiesRepository;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly SetupRepository _setupRepository;
        private readonly SpecialitiesCategoryRepository _specialitiesCategoryRepository;
        private readonly IMapper _mapper;


        public SpecialitiesController(IMapper mapper, SpecialitiesCategoryRepository specialitiesCategoryRepository, SpecialitiesRepository specialitiesRepository, SetupRepository setupRepository, PaginatedMetaService paginatedMetaService)
        {
            _specialitiesRepository = specialitiesRepository;
            _paginatedMetaService = paginatedMetaService;
            _setupRepository = setupRepository;
            _specialitiesCategoryRepository = specialitiesCategoryRepository;
            _mapper = mapper;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var specialities = _specialitiesCategoryRepository.getQueryable().Where(e => e.is_enabled == true).ToList();
            return View(specialities);
        }

        [HttpGet]
        [Route("specialities-detail/{specialities_category_id}")]
        public IActionResult specialityCategory(long specialities_category_id)
        {
            var specialities = _specialitiesRepository.getQueryable().Where(a => a.specialities_category_id == specialities_category_id).ToList();
            SpecialitiesViewModel vm = new SpecialitiesViewModel();
            vm.specialities_details = new List<SpecialitiesDetails>();
            foreach (var spec in specialities)
            {
                var detail = _mapper.Map<SpecialitiesDetails>(spec);
                vm.specialities_details.Add(detail);
            }
            return View(vm);

        }


        [HttpGet]
        [Route("detail/{slug}")]
        public IActionResult detail(string slug)
        {
            var specialitiesDetail = _specialitiesRepository.getBySlug(slug);
            return View(specialitiesDetail);
        }
    }
}

