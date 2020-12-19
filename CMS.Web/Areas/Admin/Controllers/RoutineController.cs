using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Admin.FilterModel;
using CMS.Web.Areas.Core.FilterModel;
using CMS.Web.Areas.Core.Models;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CMS.Web.Areas.Core.Controllers
{

    [Authorize]
    [Area("admin")]
    [Route("admin/routine")]
    public class RoutineController : Controller
    {
        private readonly RoutineRepository _routineRepository;
        private readonly RoutineService _routineService;
        private readonly IMapper _mapper;
        private readonly FileHelper _fileHelper;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly ClassesRepository _classesRepository;
        private readonly FiscalYearRepository _fiscalYearRepository;


        public RoutineController(RoutineRepository routineRepository, RoutineService routineService, ClassesRepository classesRepository, FiscalYearRepository fiscalYearRepository, IMapper mapper, FileHelper fileHelper, PaginatedMetaService paginatedMetaService)
        {
            _routineRepository = routineRepository;
            _routineService = routineService;
            _mapper = mapper;
            _fileHelper = fileHelper;
            _paginatedMetaService = paginatedMetaService;
            _classesRepository = classesRepository;
            _fiscalYearRepository = fiscalYearRepository;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index(RoutineFilter filter = null)
        {
            try
            {
                var routine = _routineRepository.getQueryable();
                if (!string.IsNullOrWhiteSpace(filter.title))
                {
                    routine = routine.Where(a => a.title.Contains(filter.title));
                }

                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(routine.Count(), filter.page, filter.number_of_rows);


                routine = routine.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);

                return View(routine.ToList());
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
                var classList = _classesRepository.getQueryable().ToList();
                ViewBag.classes = new SelectList(classList, "class_id", "name");
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);

            }
            return View();
        }

        [HttpPost]
        [Route("new")]
        public IActionResult add(RoutineDto model, IFormFile image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (image != null)
                    {
                        model.image = _fileHelper.saveFileAndGetFileName(image, model.image);
                    }
                    _routineService.save(model);
                    AlertHelper.setMessage(this, "Routine saved successfully.", messageType.success);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            ViewBag.classes = new SelectList(_classesRepository.getAll(), "class_id", "name");
            return View(model);
        }
        [HttpGet]
        [Route("edit/{routine_id}")]
        public IActionResult edit(long routine_id)
        {
            try
            {
                var fiscalYearList = _fiscalYearRepository.getQueryable().ToList();
                var classList = _classesRepository.getQueryable().ToList();
                ViewBag.classes = new SelectList(classList, "class_id", "name");
                ViewBag.fiscalYears = new SelectList(fiscalYearList, "fiscal_year_id", "name");
                var routine = _routineRepository.getById(routine_id);
                RoutineDto dto = _mapper.Map<RoutineDto>(routine);

                // RouteData.Values.Remove("routine_id");
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
        public IActionResult edit(RoutineDto routineDto, IFormFile image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                     if (image != null)
                        {
                        routineDto.image = _fileHelper.saveFileAndGetFileName(image, routineDto.image);
                        }
                        _routineService.update(routineDto);
                    AlertHelper.setMessage(this, "Routine updated successfully.");
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(routineDto);
        }

        [HttpGet]
        [Route("delete/{routine_id}")]
        public IActionResult delete(long routine_id)
        {
            try
            {
                _routineService.delete(routine_id);
                AlertHelper.setMessage(this, "Routine deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

    }

}