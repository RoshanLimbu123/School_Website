using CMS.Core.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Controllers
{
    [Route("member")]
    public class MemberController : BaseController
    {
        private readonly MembersRepository _membersRepo;
        public MemberController(MembersRepository membersRepo)
        {
            _membersRepo = membersRepo;
        }
        [Route("")]
        [Route("index")]
        public IActionResult index()
        {
            var members = _membersRepo.getQueryable().OrderBy(a => a.Designation.position).ToList();
            ViewBag.members = members;
            return View();
        }
    }
}
