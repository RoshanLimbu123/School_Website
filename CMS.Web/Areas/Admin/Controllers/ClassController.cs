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
    [Route("admin/classes")]
    public class ClassController : Controller
    {
        private readonly ClassesRepository _classesRepository;
        private readonly ClassesService _classesService;
        private readonly IMapper _mapper;
        private readonly FileHelper _fileHelper;
        private readonly PaginatedMetaService _paginatedMetaService;


        public ClassController(ClassesRepository classesRepository, ClassesService classesService, IMapper mapper, FileHelper fileHelper, PaginatedMetaService paginatedMetaService)
        {
            _classesRepository = classesRepository;
            _classesService = classesService;
            _mapper = mapper;
            _fileHelper = fileHelper;
            _paginatedMetaService = paginatedMetaService;


        }
        [Route("")]
        [Route("index")]
        public IActionResult Index(ClassesFilter filter = null)
        {
            try
            {
                var classes = _classesRepository.getQueryable();

                if (!string.IsNullOrWhiteSpace(filter.title))
                {
                    classes = classes.Where(a => a.name.Contains(filter.title));
                }
                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(classes.Count(), filter.page, filter.number_of_rows);


                classes = classes.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);

                return View(classes.ToList());
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
                ClassesModel classesModel = new ClassesModel();
                return View(classesModel);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [Route("new")]
        public IActionResult add(ClassesDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _classesService.save(model);
                    AlertHelper.setMessage(this, "Class saved successfully.", messageType.success);
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
        [Route("edit/{class_id}")]
        public IActionResult edit(long class_id)
        {
            try
            {
                var classes = _classesRepository.getById(class_id);
                ClassesDto dto = _mapper.Map<ClassesDto>(classes);

                // RouteData.Values.Remove("class_id");
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
        public IActionResult edit(ClassesDto classesDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _classesService.update(classesDto);
                    AlertHelper.setMessage(this, "Class upated successfully.");
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(classesDto);
        }
        [HttpGet]
        [Route("delete/{class_id}")]
        public IActionResult delete(long class_id)
        {
            try
            {
                _classesService.delete(class_id);
                AlertHelper.setMessage(this, "Class deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

    }

}

