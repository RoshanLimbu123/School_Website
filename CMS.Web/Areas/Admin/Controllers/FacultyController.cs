using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Admin.FilterModel;
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
        [Route("admin/faculty")]
        public class FacultyController : Controller
        {
            private readonly FacultyRepository _facultyRepository;
            private readonly FacultyService _facultyService;
            private readonly IMapper _mapper;
            private readonly FileHelper _fileHelper;
            private readonly PaginatedMetaService _paginatedMetaService;


            public FacultyController(FacultyRepository facultyRepository, FacultyService facultyService, IMapper mapper, FileHelper fileHelper, PaginatedMetaService paginatedMetaService)
            {
            _facultyRepository = facultyRepository;
            _facultyService = facultyService;
                _mapper = mapper;
                _fileHelper = fileHelper;
                _paginatedMetaService = paginatedMetaService;


            }
            [Route("")]
            [Route("index")]
            public IActionResult Index(FacultyFilter filter = null)
            {
                try
                {
                    var examterm = _facultyRepository.getQueryable();

                    if (!string.IsNullOrWhiteSpace(filter.title))
                    {
                        examterm = examterm.Where(a => a.name.Contains(filter.title));
                    }


                    ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(examterm.Count(), filter.page, filter.number_of_rows);


                    examterm = examterm.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);

                    return View(examterm.ToList());
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
                    FacultyModel facultyModel = new FacultyModel();
                    return View(facultyModel);
                }
                catch (Exception ex)
                {
                    AlertHelper.setMessage(this, ex.Message, messageType.error);
                    return RedirectToAction("index");
                }
            }
            [HttpPost]
            [Route("new")]
            public IActionResult add(FacultyDto model)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                       
                    _facultyService.save(model);
                        AlertHelper.setMessage(this, "Faculty saved successfully.", messageType.success);
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
            [Route("edit/{faculty_id}")]
            public IActionResult edit(long faculty_id)
            {
                try
                {
                    var faculty = _facultyRepository.getById(faculty_id);
                    FacultyDto dto = _mapper.Map<FacultyDto>(faculty);

                // RouteData.Values.Remove("faculty_id");
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
            public IActionResult edit(FacultyDto facultyDto)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                    _facultyService.update(facultyDto);
                        AlertHelper.setMessage(this, "Faculty upated successfully.");
                        return RedirectToAction("index");
                    }
                }
                catch (Exception ex)
                {
                    AlertHelper.setMessage(this, ex.Message, messageType.error);
                }
                return View(facultyDto);
            }

            [HttpGet]
            [Route("delete/{faculty_id}")]
            public IActionResult delete(long faculty_id)
            {
                try
                {
                _facultyService.delete(faculty_id);
                    AlertHelper.setMessage(this, "Faculty deleted successfully.", messageType.success);
                }
                catch (Exception ex)
                {
                    AlertHelper.setMessage(this, ex.Message, messageType.error);
                }
                return RedirectToAction(nameof(Index));
            }

        }

    }
