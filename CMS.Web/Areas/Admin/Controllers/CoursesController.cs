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

namespace CMS.Web.Areas.Core.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/course")]
    public class CoursesController : Controller
    {
        private readonly CoursesRepository _productRepo;
        private readonly CoursesService _productService;
        private readonly FileHelper _fileHelper;
        private readonly IMapper _mapper;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly FacultyRepository _facultyRepository;

        public CoursesController(CoursesRepository productRepo, CoursesService productService, IMapper mapper, FileHelper fileHelper, PaginatedMetaService paginatedMetaService, FacultyRepository facultyRepository)
        {
            _productRepo = productRepo;
            _productService = productService;
            _mapper = mapper;
            _fileHelper = fileHelper;
            _paginatedMetaService = paginatedMetaService;
            _facultyRepository = facultyRepository;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index(ProductFilter filter = null)
        {
            try
            {
                var courses = _productRepo.getQueryable();

                if (!string.IsNullOrWhiteSpace(filter.title))
                {
                    courses = courses.Where(a => a.name.Contains(filter.title));
                }

                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(courses.Count(), filter.page, filter.number_of_rows);


                courses = courses.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);

                var coursesDetails = courses.ToList();

                CourseIndexViewModel productIndexVM = getProductIndexVM(coursesDetails);

                return View(productIndexVM);
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
            ViewBag.faculty = new SelectList(_facultyRepository.getAll(), "faculty_id", "name");
            return View();
        }

        [HttpPost]
        [Route("new")]
        public IActionResult add(CoursesDto product_dto, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        product_dto.file_name = _fileHelper.saveImageAndGetFileName(file, product_dto.name);
                    }
                    _productService.save(product_dto);
                    AlertHelper.setMessage(this, "Courses saved successfully.", messageType.success);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            ViewBag.faculty = new SelectList(_facultyRepository.getAll(), "faculty_id", "name");
            return View(product_dto);
        }

        [HttpGet]
        [Route("edit/{product_id}")]
        public IActionResult edit(long product_id)
        {
            try
            {
                ViewBag.faculty = new SelectList(_facultyRepository.getAll(), "faculty_id", "name");
                var product = _productRepo.getById(product_id);
                CoursesDto dto = _mapper.Map<CoursesDto>(product);

                RouteData.Values.Remove("product_id");
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
        public IActionResult edit(CoursesDto product_dto, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string fileName = product_dto.name;
                        product_dto.file_name = _fileHelper.saveImageAndGetFileName(file, fileName);

                    }
                    _productService.update(product_dto);
                    AlertHelper.setMessage(this, "Course updated successfully.");
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            ViewBag.faculty = new SelectList(_facultyRepository.getAll(), "faculty_id", "name");
            return View(product_dto);
        }

        [HttpGet]
        [Route("enable/{product_id}")]
        public IActionResult enable(long product_id)
        {
            try
            {
                _productService.enable(product_id);
                AlertHelper.setMessage(this, "Course enabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("disable/{product_id}")]
        public IActionResult disable(long product_id)
        {
            try
            {
                _productService.disable(product_id);
                AlertHelper.setMessage(this, "Course disabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("delete/{product_id}")]
        public IActionResult delete(long product_id)
        {
            try
            {
                _productService.delete(product_id);
                AlertHelper.setMessage(this, "Course deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        private CourseIndexViewModel getProductIndexVM(List<Courses> coursesDetails)
        {
            CourseIndexViewModel vm = new CourseIndexViewModel();
            vm.courses = new List<CourseDetail>();

            foreach (var product in coursesDetails)
            {
                var convertedProduct = _mapper.Map<CourseDetail>(product);
                vm.courses.Add(convertedProduct);
            }

            return vm;
        }
    }
}