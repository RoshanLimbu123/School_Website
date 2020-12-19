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
using CMS.Web.Areas.Core.ViewModels;
using CMS.Web.Controllers;
using CMS.Web.Exceptions;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Areas.Core.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/outlet")]
    public class Outlet : BaseController
    {
        private OutletService _outletService;
        private OutletRepository _outletRepository;
        private readonly PaginatedMetaService _paginatedMetaService;
        private IMapper _mapper;
        private FileHelper _fileHelper;
        public Outlet(FileHelper fileHelper, IMapper mapper, OutletService outletService, OutletRepository outletRepository, PaginatedMetaService paginatedMetaService)
        {
            _outletService = outletService;
            _outletRepository = outletRepository;
            _paginatedMetaService = paginatedMetaService;
            _mapper = mapper;
            _fileHelper = fileHelper;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index(OutletFilter filter = null)
        {
            try
            {
                var files = _outletRepository.getQueryable();
                if (!string.IsNullOrWhiteSpace(filter.title))
                {
                    files = files.Where(a => a.title.Contains(filter.title));
                }
                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(files.Count(), filter.page, filter.number_of_rows);
                files = files.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);
                var file = files.ToList();
                var filesIndexVM = getViewModelFrom(file);
                return View(filesIndexVM);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return Redirect("index");
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
        public IActionResult add(OutletModel model, IFormFile file)
        {
            try
            {
                if (file == null)
                {
                    throw new CustomException("File must be provided.");
                }

                if (ModelState.IsValid)
                {
                    OutletDto outletDto  = new OutletDto();
                    outletDto.title = model.title;

                    outletDto.file_name = _fileHelper.saveFileAndGetFileName(file, model.title);
                    outletDto.description = model.description;
                    outletDto.address = model.address;
                    outletDto.is_enabled = model.is_enabled;

                    _outletService.save(outletDto);
                    AlertHelper.setMessage(this, "Outlet saved successfully.", messageType.success);
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
        [Route("edit/{outlet_id}")]
        public IActionResult edit(long outlet_id)
        {
            try
            {
                CMS.Core.Entity.Outlet outlet = _outletRepository.getById(outlet_id);
                OutletModel outletModel = _mapper.Map<OutletModel>(outlet);
                return View(outletModel);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }

        }

        [HttpPost]
        [Route("edit")]
        public IActionResult edit(OutletModel model, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    OutletDto outletDto = new OutletDto();

                    outletDto.outlet_id = model.outlet_id;
                    outletDto.title = model.title;
                    outletDto.description = model.description;
                    outletDto.address = model.address;

                    if (file != null)
                    {
                        outletDto.file_name = _fileHelper.saveFileAndGetFileName(file, model.title);
                    }

                    outletDto.is_enabled = model.is_enabled;
                    _outletService.update(outletDto);
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
        [Route("enable/{outlet_id}")]
        public IActionResult enable(long outlet_id)
        {
            try
            {
                _outletService.enable(outlet_id);
                AlertHelper.setMessage(this, "Outlet enabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("disable/{outlet_id}")]
        public IActionResult disable(long outlet_id)
        {
            try
            {
                _outletService.disable(outlet_id);
                AlertHelper.setMessage(this, "Outlet disabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("delete/{outlet_id}")]
        public IActionResult delete(long outlet_id)
        {
            try
            {
                _outletService.delete(outlet_id);
                AlertHelper.setMessage(this, "Outlet deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        private OutletIndexViewModel getViewModelFrom(List<CMS.Core.Entity.Outlet> outlets)
        {
            OutletIndexViewModel vm = new OutletIndexViewModel();
            vm.outlet_details = new List<OutletDetailModel>();
            foreach (var file in outlets)
            {
                var fileDetail = _mapper.Map<OutletDetailModel>(file);
                vm.outlet_details.Add(fileDetail);
            }

            return vm;
        }
    }
}