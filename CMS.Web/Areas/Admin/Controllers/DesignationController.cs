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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/Designation")]
    public class DesignationController : Controller
    {
        private readonly DesignationRepository _designationRepository;
        private readonly DesignationService _designationService;
        private readonly IMapper _mapper;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly FileHelper _fileHelper;

        public DesignationController(DesignationRepository designationRepository, DesignationService designationService, IMapper mapper, PaginatedMetaService paginatedMetaService, FileHelper fileHelper)
        {
            _designationRepository = designationRepository;
            _designationService = designationService;
            _mapper = mapper;
            _paginatedMetaService = paginatedMetaService;
            _fileHelper = fileHelper;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index(DesignationFilter filter = null)
        {
            try
            {
                var designation = _designationRepository.getQueryable();

                if (!string.IsNullOrWhiteSpace(filter.title))
                {
                    designation = designation.Where(a => a.name.Contains(filter.title));
                }
                

                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(designation.Count(), filter.page, filter.number_of_rows);


                designation = designation.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);

                return View(designation.OrderBy(a => a.position).ToList());
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
            return View();
        }

        [HttpPost]
        [Route("new")]
        public IActionResult add(DesignationDto model, IFormFile file = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string fileName = model.name;
                        model.name = _fileHelper.saveImageAndGetFileName(file, fileName);

                    }
                    _designationService.save(model);
                    AlertHelper.setMessage(this, "Designation saved successfully.", messageType.success);
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
        [Route("edit/{Designation_id}")]
        public IActionResult edit(long Designation_id)
        {
            try
            {
                var designation = _designationRepository.getById(Designation_id);
                DesignationDto dto = _mapper.Map<DesignationDto>(designation);

                RouteData.Values.Remove("Designation_id");
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
        public IActionResult edit(DesignationDto Designation_dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _designationService.update(Designation_dto);
                    AlertHelper.setMessage(this, "Designation updated successfully.");
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(Designation_dto);
        }

        [HttpGet]
        [Route("delete/{designation_id}")]
        public IActionResult delete(long designation_id)
        {
            try
            {
                _designationService.delete(designation_id);
                AlertHelper.setMessage(this, "Designation deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
