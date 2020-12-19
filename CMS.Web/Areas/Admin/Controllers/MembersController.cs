using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Admin.FilterModel;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/members")]
    public class MembersController : Controller
    {
        private readonly MembersRepository _membersRepository;
        private readonly MembersService _membersService;
        private readonly IMapper _mapper;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly FileHelper _fileHelper;
        private readonly DesignationRepository _designationRepository;
        private readonly FiscalYearRepository _fiscalYearRepository;

        public MembersController(MembersService membersService, MembersRepository membersRepository, IMapper mapper, PaginatedMetaService paginatedMetaService, FileHelper fileHelper, DesignationRepository designationRepository,FiscalYearRepository fiscalYearRepository)
        {
            _membersRepository = membersRepository;
            _membersService = membersService;
            _mapper = mapper;
            _paginatedMetaService = paginatedMetaService;
            _fiscalYearRepository = fiscalYearRepository;
            _designationRepository = designationRepository;
            _fileHelper = fileHelper;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index(MembersFilter filter = null)
        {
            try
            {
                var Members = _membersRepository.getQueryable();

                if (!string.IsNullOrWhiteSpace(filter.title))
                {
                    Members = Members.Where(a => a.full_name.Contains(filter.title));
                }

                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(Members.Count(), filter.page, filter.number_of_rows);

                Members = Members.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);

                return View(Members.OrderByDescending(a => a.member_id).ToList());
            }

            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return Redirect("/admin");

            }

        }
        [HttpGet]
        [Route("new")]
        public IActionResult add()
        {
            try
            {
                var fiscalYearList = _fiscalYearRepository.getQueryable().ToList();
                var designationList = _designationRepository.getQueryable().ToList();
                ViewBag.designations = new SelectList(designationList, "Designation_id","name");
                ViewBag.fiscalYears = new SelectList(fiscalYearList, "fiscal_year_id", "name");
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
               
            }
            return View();
        }

        [HttpPost]
        [Route("new")]
        public IActionResult add(MemberDto model, IFormFile image_url)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (image_url != null)
                    {
                        model.image_url = _fileHelper.saveImageAndGetFileName(image_url, model.full_name);
                    }
                    _membersService.save(model);
                    AlertHelper.setMessage(this, "Member Saved Successfully", messageType.success);
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
        [Route("edit/{members_id}")]
        public IActionResult edit(long members_id)
        {
            try
            {
                var fiscalYearList = _fiscalYearRepository.getQueryable().ToList();
                var designationList = _designationRepository.getQueryable().ToList();
                ViewBag.designations = new SelectList(designationList, "Designation_id", "name");
                ViewBag.fiscalYears = new SelectList(fiscalYearList, "fiscal_year_id", "name");

                var Members = _membersRepository.getById(members_id);
                MemberDto dto = _mapper.Map<MemberDto>(Members);
                return View(dto);
            }
            catch (Exception ex)
            {

                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }

        }

        [HttpPost]
        [Route("edit")]
        public IActionResult edit(MemberDto memberDto,IFormFile image_url)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(image_url != null)
                    {
                        memberDto.image_url = _fileHelper.saveImageAndGetFileName(image_url, memberDto.full_name);
                    }
                    _membersService.update(memberDto);
                    AlertHelper.setMessage(this, "Members Updated Successfully.", messageType.success);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(memberDto);
        }
    

         [HttpGet]
         [Route("delete/{members_id}")]
         public IActionResult delete(long members_id)
        {
            try
            {
                _membersService.delete(members_id);
                AlertHelper.setMessage(this, "Members Deleted Successfully.", messageType.success);

            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this,ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
