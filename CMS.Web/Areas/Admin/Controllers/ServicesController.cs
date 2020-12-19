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

namespace CMS.Web.Areas.Core.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/services")]
    public class ServicesController : Controller
    {
        private readonly ServicesRepository _servicesRepository;
        private readonly ServicesService _servicesService;
        private readonly IMapper _mapper;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly FileHelper _fileHelper;
        public ServicesController(ServicesRepository servicesRepository, ServicesService servicesService, IMapper mapper, PaginatedMetaService paginatedMetaService, FileHelper fileHelper)
        {
            _servicesRepository = servicesRepository;
            _servicesService = servicesService;
            _mapper = mapper;
            _paginatedMetaService = paginatedMetaService;
            _fileHelper = fileHelper;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index(ServicesFilter filter = null)
        {
            var serv = _servicesRepository.getQueryable();

            if (!string.IsNullOrWhiteSpace(filter.title))
            {
                serv = serv.Where(a => a.name.Contains(filter.title));
            }

            ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(serv.Count(), filter.page, filter.number_of_rows);


            serv = serv.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);

            var serviceDetail = serv.ToList();

            var serviceIndexVM = getViewModelFrom(serviceDetail);
            return View(serviceIndexVM);
        }

        private object getViewModelFrom(List<Services> serviceDetail)
        {
            ServicesIndexViewModel gvm = new ServicesIndexViewModel();
            gvm.servicesDetails = new List<ServicesDetails>();
            foreach (var news in serviceDetail)
            {
                var galleryDetail = _mapper.Map<ServicesDetails>(news);
                gvm.servicesDetails.Add(galleryDetail);
            }

            return gvm;
        }
        [HttpGet]
        [Route("new")]
        public IActionResult add()
        {
            return View();
        }

        [HttpPost]
        [Route("new")]

        public IActionResult add(ServicesDto servicedto, IFormFile file = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string fileName = servicedto.name;
                        servicedto.image = _fileHelper.saveImageAndGetFileName(file, fileName);

                    }
                    _servicesService.save(servicedto);
                    AlertHelper.setMessage(this, "Service saved successfully.", messageType.success);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(servicedto);
        }
        [HttpGet]
        [Route("edit/{service_id}")]
        public IActionResult edit(long service_id)
        {
            try
            {
                var video1 = _servicesRepository.getById(service_id);
                ServicesDto dto = _mapper.Map<ServicesDto>(video1);

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
        public IActionResult edit(ServicesDto servicedto, IFormFile file = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string fileName = servicedto.name;
                        servicedto.image = _fileHelper.saveImageAndGetFileName(file, fileName);

                    }
                    _servicesService.update(servicedto);
                    AlertHelper.setMessage(this, "Service updated successfully.");
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(servicedto);
        }

        [HttpGet]
        [Route("delete/{service_id}")]
        public IActionResult delete(long service_id)
        {
            try
            {
                _servicesService.delete(service_id);
                AlertHelper.setMessage(this, "Service deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Route("enable/{service_id}")]
        public IActionResult enable(long service_id)
        {
            try
            {
                _servicesService.enable(service_id);
                AlertHelper.setMessage(this, "Service enabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("disable/{service_id}")]
        public IActionResult disable(long service_id)
        {
            try
            {
                _servicesService.disable(service_id);
                AlertHelper.setMessage(this, "Service disabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

    }



}