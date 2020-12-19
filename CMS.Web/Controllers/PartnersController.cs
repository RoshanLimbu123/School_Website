using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers
{
    [Route("partners")]
    public class PartnersController : Controller
    {
        private readonly PartnersRepository _partnersRepository;
        public PartnersController(PartnersRepository partnersRepository)
        {
            _partnersRepository = partnersRepository;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var partners = _partnersRepository.getQueryable().Where(e => e.is_active == true).ToList();
            return View(partners);
           
        }
    }
}