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
    [Route("admin/examTerm")]
    public class ExamTermController : Controller
    {
        private readonly ExamTermRepository _examTermRepository;
        private readonly ExamTermService _examTermService;
        private readonly IMapper _mapper;
        private readonly FileHelper _fileHelper;
        private readonly PaginatedMetaService _paginatedMetaService;


        public ExamTermController(ExamTermRepository examTermRepository, ExamTermService examTermService, IMapper mapper, FileHelper fileHelper, PaginatedMetaService paginatedMetaService)
        {
            _examTermRepository = examTermRepository;
            _examTermService = examTermService;
            _mapper = mapper;
            _fileHelper = fileHelper;
            _paginatedMetaService = paginatedMetaService;


        }
        [Route("")]
        [Route("index")]
      
        public IActionResult Index(ExamTermFilter filter = null)
        {
            try
            {
                var examterm = _examTermRepository.getQueryable();

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
                ExamTermModel examTermModel = new ExamTermModel();
                return View(examTermModel);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        [Route("new")]
        public IActionResult add(ExamtermDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    _examTermService.save(model);
                    AlertHelper.setMessage(this, "Exam Term saved successfully.", messageType.success);
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
        [Route("edit/{exam_term_id}")]
        public IActionResult edit(long exam_term_id)
        {
            try
            {
                var examterm = _examTermRepository.getById(exam_term_id);
                ExamtermDto dto = _mapper.Map<ExamtermDto>(examterm);

                // RouteData.Values.Remove("exam_term_id");
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
        public IActionResult edit(ExamtermDto examTermDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _examTermService.update(examTermDto);
                    AlertHelper.setMessage(this, "Exam Term updated successfully.");
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(examTermDto);
        }

        [HttpGet]
        [Route("delete/{exam_term_id}")]
        public IActionResult delete(long exam_term_id)
        {
            try
            {
                _examTermService.delete(exam_term_id);
                AlertHelper.setMessage(this, "Exam Term deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

    }

}

