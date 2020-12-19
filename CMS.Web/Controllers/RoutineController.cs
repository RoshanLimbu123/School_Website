using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Repository.Interface;
using CMS.Web.Areas.Admin.FilterModel;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;

namespace CMS.Web.Controllers
{
    [Route("routine")]
    public class RoutineController : Controller
    {
        private readonly RoutineRepository _routineRepository;
        private readonly ClassesRepository _classRepository;
        public RoutineController(RoutineRepository routineRepository, ClassesRepository classesRepository)
        {
            _routineRepository = routineRepository;
            _classRepository = classesRepository;

        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {

            try
            {

                var classes = _classRepository.getQueryable().Where(e => e.is_active == true).Take(3).ToList();
                ViewBag.classs = new SelectList(classes, "class_id", "name");
                return View();
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }
        [Route("get/{class_id}")]
        [HttpGet]
        public IActionResult get(long class_id)
        {
            var routine = _routineRepository.getQueryable().Where(a => a.end_date.Date > DateTime.Now.Date && a.class_id == class_id);
            return Json(routine.ToList());
        }
    }
}