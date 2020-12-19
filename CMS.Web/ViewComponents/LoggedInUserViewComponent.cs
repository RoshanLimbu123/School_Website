using CMS.User.Repository.Interface;
using CMS.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CMS.Web.ViewComponents
{
    [ViewComponent(Name = "LoggedInUser")]
    public class LoggedInUserViewComponent : ViewComponent
    {
        private readonly UserRepository _userRepo;
        private readonly AuthenticationRepository _authenticationRepo;

        public LoggedInUserViewComponent(UserRepository userRepo,AuthenticationRepository authenticationRepo)
        {
            _userRepo = userRepo;
            _authenticationRepo = authenticationRepo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            long loggedInAuthenticationId = 0;
            string authenticationId = Request.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(authenticationId))
            {
              loggedInAuthenticationId= Convert.ToInt64(authenticationId);
            }
            var user_id = _authenticationRepo.getById(loggedInAuthenticationId).type_id;

            var userDetail = _userRepo.getById(user_id);
            return View(userDetail);
        }
    }
}
