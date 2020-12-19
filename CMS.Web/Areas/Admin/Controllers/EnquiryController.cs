using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Admin.ViewModels;
using CMS.Web.Areas.Core.ViewModels;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/enquiry")]
    public class EnquiryController : Controller
    {
        private readonly EnquiryService _enquiryService;
        private readonly IMapper _mapper;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly EnquiryRepository _enquiryRepository;
       
        public EnquiryController(EnquiryService enquiryService, EnquiryRepository enquiryRepository,PaginatedMetaService paginatedMetaService, IMapper mapper)
        {
            _enquiryService = enquiryService;
            _mapper = mapper;
            _paginatedMetaService = paginatedMetaService;
            _enquiryRepository = enquiryRepository;
        }
        //[HttpPost]
        //[Route("")]
        //public IActionResult enquiry()
        //{
        //    try
        //    {
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        AlertHelper.setMessage(this, ex.Message, messageType.error);
        //        return RedirectToAction("enquiry");
        //    }
        //}
        [Route("")]
        public IActionResult enquiry()
        {
            try
            {
                var enquiries = _enquiryRepository.getQueryable().ToList();
                var enquiryVM = getViewModelFromEnquiry(enquiries);

                return View(enquiryVM);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("enquiry");
            }
        }
        [HttpGet]
        [Route("enquiryDelete/{enquiry_id}")]
        public IActionResult enquiryDelete(long enquiry_id)
        {
            try
            {
                _enquiryService.delete(enquiry_id);
                AlertHelper.setMessage(this, "Enquiry deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(enquiry));
        }

        [HttpGet]
        [Route("enquiryEdit/{enquiry_id}")]
        public IActionResult enquiryEdit(long enquiry_id)
        {
            try
            {
                var enquiries = _enquiryRepository.getById(enquiry_id);
                EnquiryDto enquiryDto = _mapper.Map<EnquiryDto>(enquiries);

                return View(enquiryDto);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("enquiry");
            }
        }
        [HttpPost]
        [Route("enquiryEdit")]
        public IActionResult enquiryEdit(EnquiryDto enquiryDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _enquiryService.update(enquiryDto);
                    AlertHelper.setMessage(this, "Enquiry updated successfully.");
                    return RedirectToAction("enquiry");

                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(enquiryDto);
        }


        private EnquiryIndexViewModel getViewModelFromEnquiry(List<CMS.Core.Entity.Enquiry> enquiries)
        {
            EnquiryIndexViewModel vm = new EnquiryIndexViewModel();
            vm.enquiry_details = new List<EnquiryDetailModel>();
            foreach (var enquiry in enquiries)
            {
                var enq = _mapper.Map<EnquiryDetailModel>(enquiry);
                vm.enquiry_details.Add(enq);
            }

            return vm;
        }
    }
}
