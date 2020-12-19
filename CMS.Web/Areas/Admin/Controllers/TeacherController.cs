using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Core.FilterModel;
using CMS.Web.Areas.Core.ViewModels;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static CMS.Web.Areas.Core.ViewModels.TeacherIndexViewModel;

namespace CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/teacher")]
    public class TeacherController : Controller
    {
        private readonly TeacherRepository _teacherRepo;
        private readonly TeacherService _teacherService;
        private readonly IMapper _mapper;
        private readonly FileHelper _fileHelper;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly ItemCategoryRepository _itemCategoryRepo;

        public TeacherController(TeacherRepository teacherRepo,TeacherService teacherService,IMapper mapper,FileHelper fileHelper,PaginatedMetaService paginatedMetaService,ItemCategoryRepository itemCategoryRepo)
        {
            _teacherRepo = teacherRepo;
            _teacherService = teacherService;
            _mapper = mapper;
            _fileHelper = fileHelper;
            _paginatedMetaService = paginatedMetaService;
            _itemCategoryRepo = itemCategoryRepo;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index(TeacherFilter filter=null)
        {
            try
            {
                var teachers = _teacherRepo.getQueryable();
                if(!string.IsNullOrWhiteSpace(filter.title))
                {
                    teachers = teachers.Where(a => a.name.Contains(filter.title));
                }
                ViewBag.pageInfo = _paginatedMetaService.GetMetaData(teachers.Count(), filter.page, filter.number_of_rows);
                teachers = teachers.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);
                var teacherDetails = teachers.ToList();
                TeacherIndexViewModel teacherIndexVM = getTeacherIndexVM(teacherDetails);
                return View(teacherIndexVM);
            }
            catch (Exception ex)
            {

                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return Redirect("/admin");
            }
        }
        [Route("new")]
        public IActionResult add()
        {
            ViewBag.item_categories = new SelectList(_itemCategoryRepo.getAll(), "item_category_id", "name");
            return View();
        }
        [HttpPost]
        [Route("new")]
        public IActionResult add(TeacherDto teacherDto,IFormFile file)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(file !=null)
                    {
                        teacherDto.file_name = _fileHelper.saveFileAndGetFileName(file, teacherDto.file_name);
                    }
                    _teacherService.save(teacherDto);
                    AlertHelper.setMessage(this, "Teacher Save Successfully", messageType.success);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            ViewBag.item_categories = new SelectList(_itemCategoryRepo.getAll(), "item_category_id", "name");
            return View(teacherDto);
        }
        [HttpGet]
        [Route("edit/{teacher_id}")]
        public IActionResult edit(long teacher_id)
        {
            try
            {
                ViewBag.item_categories = new SelectList(_itemCategoryRepo.getAll(), "item_category_id", "name");
                var teacher = _teacherRepo.getById(teacher_id);
                TeacherDto dto = _mapper.Map<TeacherDto>(teacher);
                RouteData.Values.Remove("teacher_id");
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
        public IActionResult edit(TeacherDto teacherDto,IFormFile file)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(file !=null)
                    {
                        string fileName = teacherDto.name;
                        teacherDto.file_name = _fileHelper.saveImageAndGetFileName(file, fileName);
                    }
                    _teacherService.update(teacherDto);
                    AlertHelper.setMessage(this, "Teacher Updated Successfully");
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {

                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            ViewBag.item_catagories = new SelectList(_itemCategoryRepo.getAll(), "item_category_id", "name");
            return View(teacherDto);
        }
        [HttpGet]
        [Route("enable/{teacher_id}")]
        public IActionResult enable(long teacher_id)
        {
            try
            {
                _teacherService.enable(teacher_id);
                AlertHelper.setMessage(this, "Teacher enabled successfully.", messageType.success);

            }
            catch (Exception ex)
            {

                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Route("disable/{teacher_id}")]
        public IActionResult disable(long teacher_id)
        {
            try
            {
                _teacherService.disable(teacher_id);
                AlertHelper.setMessage(this, "Teacher disabled successfully", messageType.success);
            }
            catch (Exception ex)
            {

                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Route("delete/{teacher_id}")]
        public IActionResult delete(long teacher_id)
        {
            try
            {
                _teacherService.delete(teacher_id);
                AlertHelper.setMessage(this, "Teacher Deleted successfully", messageType.success);
            }
            catch (Exception ex)
            {

                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }
        private TeacherIndexViewModel getTeacherIndexVM(List<Teacher> teacherDetails)
        {
            TeacherIndexViewModel vm = new TeacherIndexViewModel();
            vm.teachers = new List<TeacherDetail>();

            foreach (var teacher in teacherDetails)
            {
                var convertedTeacher = _mapper.Map<TeacherDetail>(teacher);
                convertedTeacher.item_category_name = teacher.item_category?.name;
                vm.teachers.Add(convertedTeacher);
            }

            return vm;
        }
    }
}
