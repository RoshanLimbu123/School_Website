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
using System.Linq;

namespace CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/testimonial")]
    public class TestimonialController : Controller
    {
        private readonly IMapper _mapper;
        private readonly TestimonialRepository _testimonialRepo;
        private readonly TestimonialService _testimonialService;
        private readonly FileHelper _fileHelper;
        private readonly PaginatedMetaService _paginatedMetaService;

        public TestimonialController(IMapper mapper, TestimonialRepository testimonialRepo, TestimonialService testimonialService, FileHelper fileHelper, PaginatedMetaService paginatedMetaService)
        {
            _mapper = mapper;
            _testimonialRepo = testimonialRepo;
            _testimonialService = testimonialService;
            _fileHelper = fileHelper;
            _paginatedMetaService = paginatedMetaService;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index(TestimonialFilter filter = null)
        {
            try
            {
                var testimonials = _testimonialRepo.getQueryable();

                if (!string.IsNullOrWhiteSpace(filter.title))
                {
                    testimonials = testimonials.Where(a => a.person_name.Contains(filter.title));
                }
                
                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(testimonials.Count(), filter.page, filter.number_of_rows);


                testimonials = testimonials.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);
                return View(testimonials.ToList());
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
        public IActionResult add(TestimonialDto model, IFormFile file = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string fileName = model.person_name;
                        model.image_name = _fileHelper.saveImageAndGetFileName(file, fileName);

                    }
                    _testimonialService.save(model);
                    AlertHelper.setMessage(this, "Testimonial saved successfully.", messageType.success);
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
        [Route("edit/{testimonial_id}")]
        public IActionResult edit(long testimonial_id)
        {
            try
            {
                var testimonial = _testimonialRepo.getById(testimonial_id);
                TestimonialDto dto = _mapper.Map<TestimonialDto>(testimonial);
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
        public IActionResult edit(TestimonialDto testimonial_dto, IFormFile file = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string fileName = testimonial_dto.person_name;
                        testimonial_dto.image_name = _fileHelper.saveImageAndGetFileName(file, fileName);

                    }
                    _testimonialService.update(testimonial_dto);
                    AlertHelper.setMessage(this, "Testimonial updated successfully.");
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(testimonial_dto);
        }

        [HttpGet]
        [Route("delete/{testimonial_id}")]
        public IActionResult delete(long testimonial_id)
        {
            try
            {
                _testimonialService.delete(testimonial_id);
                AlertHelper.setMessage(this, "Testimonial deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("hide/{testimonial_id}")]
        public IActionResult makeInvisible(long testimonial_id)
        {
            try
            {
                _testimonialService.makeInvisible(testimonial_id);
                AlertHelper.setMessage(this, "Testimonial hidden successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("show/{testimonial_id}")]
        public IActionResult makeVisible(long testimonial_id)
        {
            try
            {
                _testimonialService.makeVisible(testimonial_id);
                AlertHelper.setMessage(this, "Testimonial shown successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}