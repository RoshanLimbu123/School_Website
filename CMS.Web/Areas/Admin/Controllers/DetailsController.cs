using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Admin.ViewModels;
using CMS.Web.Areas.Core.Models;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/details")]
    public class DetailsController : Controller
    {
        private readonly DetailsRepository _detailsRepository;
        private readonly DetailsService _detailsService;
        private readonly IMapper _mapper;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly FileHelper _fileHelper;

        public DetailsController(DetailsRepository detailsRepository,DetailsService detailsService,IMapper mapper,PaginatedMetaService paginatedMetaService,FileHelper fileHelper)
        {
            _detailsRepository = detailsRepository;
            _detailsService = detailsService;
            _mapper = mapper;
            _paginatedMetaService = paginatedMetaService;
            _fileHelper = fileHelper;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            try
            {
                var details = _detailsRepository.getQueryable();

                var detail = details.ToList(); ;
                var detailIndexVM = getViewModelFrom(detail);
                return View(detailIndexVM);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("new")]
        public IActionResult add()
        {
            return View();
        }

        [HttpPost]
        [Route("new")]
        public IActionResult add(DetailsModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DetailsDto dto = new DetailsDto();
                    dto.details_id = model.details_id;
                    dto.value0 = model.value0;
                    dto.value1 = model.value1;
                    dto.value2 = model.value2;
                    dto.value3 = model.value3;
                    dto.value4 = model.value4;

                    _detailsService.save(dto);
                    AlertHelper.setMessage(this, "Details saved successfully.", messageType.success);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }

            return View(model);
        }

        [HttpGet]
        [Route("edit/{details_id}")]
        public IActionResult edit(long details_id)
        {
            try
            {
                Details details = _detailsRepository.getById(details_id);
                DetailsModel detailsModel = _mapper.Map<DetailsModel>(details);

                return View(detailsModel);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult edit(DetailsModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DetailsDto dto = new DetailsDto();
                    dto.details_id = model.details_id;
                    dto.value0 = model.value0;
                    dto.value1 = model.value1;
                    dto.value2 = model.value2;
                    dto.value3 = model.value3;
                    dto.value4 = model.value4;

                    _detailsService.update(dto);
                    AlertHelper.setMessage(this, "Details updated successfully.");
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(model);
        }

        [HttpGet]
        [Route("delete/{details_id}")]
        public IActionResult delete(long details_id)
        {
            try
            {
                _detailsService.delete(details_id);
                AlertHelper.setMessage(this, "Details deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        private DetailsIndexViewModel getViewModelFrom(List<CMS.Core.Entity.Details> details)
        {
            DetailsIndexViewModel vm = new DetailsIndexViewModel();
            vm.details = new List<DetailsDetailModel>();
            foreach (var detail in details)
            {
                var Detail = _mapper.Map<DetailsDetailModel>(detail);
                vm.details.Add(Detail);
            }

            return vm;
        }
    }
}