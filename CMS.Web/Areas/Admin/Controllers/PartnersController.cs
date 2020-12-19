using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Admin.FilterModel;
using CMS.Web.Controllers;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Areas.Admin.Controllers
{

    [Authorize]
    [Area("admin")]
    [Route("admin/partners")]
    public class PartnersController : Controller
    {
        private readonly PartnersRepository _partnersRepository;
        private readonly PartnersService _partnersService;
        private readonly IMapper _mapper;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly FileHelper _fileHelper;
        public PartnersController(PartnersRepository partnersRepository, PartnersService partnersService, IMapper mapper, PaginatedMetaService paginatedMetaService, FileHelper fileHelper)
        {
            _partnersRepository = partnersRepository;
            _partnersService = partnersService;
            _mapper = mapper;
            _paginatedMetaService = paginatedMetaService;
            _fileHelper = fileHelper;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index(PartnersFilter filter = null)
        {

            try
            {
                var partners = _partnersRepository.getQueryable();

                if (!string.IsNullOrWhiteSpace(filter.title))
                {
                    partners = partners.Where(a => a.name.Contains(filter.title));
                }

                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(partners.Count(), filter.page, filter.number_of_rows);

                partners = partners.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);

                return View(partners.OrderByDescending(a => a.partners_id).ToList());

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
        public IActionResult add(PartnersDto partnersDto, IFormFile file = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string fileName = partnersDto.name;
                        partnersDto.logo_name = _fileHelper.saveImageAndGetFileName(file, fileName);

                    }
                    _partnersService.save(partnersDto);
                    AlertHelper.setMessage(this, "Partners saved successfully.", messageType.success);

                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(partnersDto);
        }


        [HttpGet]
        [Route("edit/{partners_id}")]
        public IActionResult edit(long partners_id)
        {
            try
            {
                var partners = _partnersRepository.getById(partners_id);
                PartnersDto dto = _mapper.Map<PartnersDto>(partners);

                RouteData.Values.Remove("partners_id");
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
        public IActionResult edit(PartnersDto partnersDto, IFormFile file = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string fileName = partnersDto.name;
                        partnersDto.logo_name = _fileHelper.saveImageAndGetFileName(file, fileName);

                    }
                    _partnersService.update(partnersDto);
                    AlertHelper.setMessage(this, "Partners updated successfully.");
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(partnersDto);
        }

        [HttpGet]
        [Route("delete/{partners_id}")]
        public IActionResult delete(long partners_id)
        {
            try
            {
                _partnersService.delete(partners_id);
                AlertHelper.setMessage(this, "Partners deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("enable/{partners_id}")]
        public IActionResult enable(long partners_id)
        {
            try
            {
                _partnersService.enable(partners_id);
                AlertHelper.setMessage(this, "Partners enabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("disable/{partners_id}")]
        public IActionResult disable(long partners_id)
        {
            try
            {
                _partnersService.disable(partners_id);
                AlertHelper.setMessage(this, "Partners disabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
