using CMS.Core.Repository.Interface;
using CMS.Web.FilterModel;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CMS.Web.Controllers
{
    [Route("outlet")]

    public class OutletController : Controller
    {
        private readonly OutletRepository _outletRepository;
        private readonly PaginatedMetaService _paginatedMetaService;

        public OutletController(OutletRepository outletRepository, PaginatedMetaService paginatedMetaService)
        {
            _outletRepository = outletRepository;
            _paginatedMetaService = paginatedMetaService;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index(FileFilter filter=null)
        {
            var files = _outletRepository.getQueryable().Where(a => a.is_enabled == true);

            ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(files.Count(), filter.page, filter.number_of_rows);


            files = files.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);

            return View(files.ToList());
        }
    }
}