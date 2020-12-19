using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Core.FilterModel;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Areas.Core.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/career")]
    public class CareerController : Controller
    {
        private readonly CareerRepository _careerRepo;
        private readonly CareerService _careerService;
        private readonly IMapper _mapper;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly FileHelper _fileHelper;

        public CareerController(CareerRepository careerRepo, CareerService careerService, IMapper mapper, PaginatedMetaService paginatedMetaService, FileHelper fileHelper)
        {
            _careerRepo = careerRepo;
            _careerService = careerService;
            _mapper = mapper;
            _paginatedMetaService = paginatedMetaService;
            _fileHelper = fileHelper;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index(CareerFilter filter = null)
        {
            try
            {
                var careers = _careerRepo.getQueryable();

                if (!string.IsNullOrWhiteSpace(filter.title))
                {
                    careers = careers.Where(a => a.title.Contains(filter.title));
                }

                careers = careers.Where(a => a.opening_date >= filter.from_date && a.opening_date <= filter.to_date);

                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(careers.Count(), filter.page, filter.number_of_rows);


                careers = careers.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);

                return View(careers.ToList());
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
        public IActionResult add(CareerDto model, IFormFile file = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string fileName = model.title;
                        model.image_name = _fileHelper.saveImageAndGetFileName(file, fileName);

                    }
                    _careerService.save(model);
                    AlertHelper.setMessage(this, "Career saved successfully.", messageType.success);
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
        [Route("edit/{career_id}")]
        public IActionResult edit(long career_id)
        {
            try
            {
                var career = _careerRepo.getById(career_id);
                CareerDto dto = _mapper.Map<CareerDto>(career);
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
        public IActionResult edit(CareerDto career_dto, IFormFile file = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string fileName = career_dto.title;
                        career_dto.image_name = _fileHelper.saveImageAndGetFileName(file, fileName);

                    }
                    _careerService.update(career_dto);
                    AlertHelper.setMessage(this, "Career updated successfully.");
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(career_dto);
        }


        [HttpGet]
        [Route("close/{career_id}")]
        public IActionResult close(long career_id)
        {
            try
            {
                _careerService.close(career_id);
                AlertHelper.setMessage(this, "Career closed successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("unclose/{career_id}")]
        public IActionResult disable(long career_id)
        {
            try
            {
                _careerService.unclose(career_id);
                AlertHelper.setMessage(this, "Career unclosed successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Route("delete/{career_id}")]
        public IActionResult delete(long career_id)
        {
            try
            {
                _careerService.delete(career_id);
                AlertHelper.setMessage(this, "Career deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}