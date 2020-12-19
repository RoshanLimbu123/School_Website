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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/FiscalYear")]
    public class FiscalYearController : Controller
    {
        private readonly FiscalYearRepository _fiscalYearRepository;
        private readonly FiscalYearService _fiscalYearService;
        private readonly IMapper _mapper;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly FileHelper _fileHelper;
        public FiscalYearController(FiscalYearRepository fiscalYearRepository, FiscalYearService fiscalYearService,IMapper mapper, PaginatedMetaService paginatedMetaService,FileHelper fileHelper)
        {
            _fiscalYearRepository = fiscalYearRepository;
            _fiscalYearService = fiscalYearService;
            _mapper = mapper;
            _paginatedMetaService = paginatedMetaService;
            _fileHelper = fileHelper;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index(FiscalYearFilter filter = null)
        {
            try
            {
                var fiscalyear = _fiscalYearRepository.getQueryable();

                if (!string.IsNullOrWhiteSpace(filter.title))
                {
                    fiscalyear = fiscalyear.Where(a => a.name.Contains(filter.title));
                }


                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(fiscalyear.Count(), filter.page, filter.number_of_rows);


                fiscalyear = fiscalyear.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);

                return View(fiscalyear.ToList());
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return Redirect("/admin");
            }
        }
        [HttpGet]
        [Route("makeCurrent/{fiscal_year_id}")]
        public IActionResult makeCurrent(long fiscal_year_id)
        {
            try
            {
                _fiscalYearService.active(fiscal_year_id);
                return RedirectToAction("index");
            }
            catch (Exception)
            {

                throw;
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
        public IActionResult add(FiscalYearDto model, IFormFile file = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string fileName = model.name;
                        model.name = _fileHelper.saveImageAndGetFileName(file, fileName);

                    }
                    _fiscalYearService.save(model);
                    AlertHelper.setMessage(this, "Fiscal Year saved successfully.", messageType.success);
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
        [Route("edit/{FiscalYear_id}")]
        public IActionResult edit(long FiscalYear_id)
        {
            try
            {
                var fiscalYear = _fiscalYearRepository.getById(FiscalYear_id);
                FiscalYearDto dto = _mapper.Map<FiscalYearDto>(fiscalYear);

               // RouteData.Values.Remove("FiscalYear_id");
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
        public IActionResult edit(FiscalYearDto FiscalYear_dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _fiscalYearService.update(FiscalYear_dto);
                    AlertHelper.setMessage(this, "Fiscal Year updated successfully.");
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(FiscalYear_dto);
        }

        [HttpGet]
        [Route("delete/{FiscalYear_id}")]
        public IActionResult delete(long FiscalYear_id)
        {
            try
            {
                _fiscalYearService.delete(FiscalYear_id);
                AlertHelper.setMessage(this, "Fiscal Year deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
