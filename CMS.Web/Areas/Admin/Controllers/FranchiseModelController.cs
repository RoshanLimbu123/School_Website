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
    [Route("admin/franchise")]
    public class FranchiseModel : BaseController
    {
        private FranchiseModelService _franchiseModelService;
        private FranchiseModelRepository _franchiseModelRepository;
        private readonly PaginatedMetaService _paginatedMetaService;
        private IMapper _mapper;
        private FileHelper _fileHelper;
        public FranchiseModel(FileHelper fileHelper, IMapper mapper, FranchiseModelService franchiseModelservice, FranchiseModelRepository franchiseModelRepository, PaginatedMetaService paginatedMetaService)
        {
            _franchiseModelService = franchiseModelservice;
            _franchiseModelRepository = franchiseModelRepository;
            _paginatedMetaService = paginatedMetaService;
            _mapper = mapper;
            _fileHelper = fileHelper;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index(FranchiseModelFilter filter = null)
        {
            try
            {
                var files = _franchiseModelRepository.getQueryable();
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
        public IActionResult add(FranchiseModels model, IFormFile file)
        {
            try
            {
                if (file == null)
                {
                    throw new CustomException("File must be provided.");
                }

                if (ModelState.IsValid)
                {
                    FranchiseModelDto franchiseModelDto  = new FranchiseModelDto();
                    franchiseModelDto.title = model.title;

                    franchiseModelDto.file_name = _fileHelper.saveFileAndGetFileName(file, model.title);
                    franchiseModelDto.description = model.description;
                    franchiseModelDto.is_enabled = model.is_enabled;

                    _franchiseModelService.save(franchiseModelDto);
                    AlertHelper.setMessage(this, "Franchise saved successfully.", messageType.success);
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
        [Route("edit/{franchise_model_id}")]
        public IActionResult edit(long franchise_model_id)
        {
            try
            {
                CMS.Core.Entity.FranchiseModel franchiseModel = _franchiseModelRepository.getById(franchise_model_id);
                FranchiseModels outletModel = _mapper.Map<FranchiseModels>(franchiseModel);
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
        public IActionResult edit(FranchiseModels model, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FranchiseModelDto franchiseModelDto = new FranchiseModelDto();

                    franchiseModelDto.franchise_model_id = model.franchise_model_id;
                    franchiseModelDto.title = model.title;
                    franchiseModelDto.description = model.description;

                    if (file != null)
                    {
                        franchiseModelDto.file_name = _fileHelper.saveFileAndGetFileName(file, model.title);
                    }

                    franchiseModelDto.is_enabled = model.is_enabled;
                    _franchiseModelService.update(franchiseModelDto);
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
        [Route("enable/{franchise_model_id}")]
        public IActionResult enable(long franchise_model_id)
        {
            try
            {
                _franchiseModelService.enable(franchise_model_id);
                AlertHelper.setMessage(this, "Franchise enabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("disable/{franchise_model_id}")]
        public IActionResult disable(long franchise_model_id)
        {
            try
            {
                _franchiseModelService.disable(franchise_model_id);
                AlertHelper.setMessage(this, "Franchise disabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("delete/{franchise_model_id}")]
        public IActionResult delete(long franchise_model_id)
        {
            try
            {
                _franchiseModelService.delete(franchise_model_id);
                AlertHelper.setMessage(this, "Franchise deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        private FranchiseModelIndexViewModel getViewModelFrom(List<CMS.Core.Entity.FranchiseModel> franchiseModels)
        {
            FranchiseModelIndexViewModel vm = new FranchiseModelIndexViewModel();
            vm.franchiseDetailModels = new List<franchiseDetailModel>();
            foreach (var file in franchiseModels)
            {
                var fileDetail = _mapper.Map<franchiseDetailModel>(file);
                vm.franchiseDetailModels.Add(fileDetail);
            }

            return vm;
        }
    }
}