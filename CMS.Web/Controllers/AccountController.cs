using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CMS.User.Service.Interface;
using CMS.Web.Helpers;
using CMS.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers
{
    using userNS = CMS.User.Service.Interface;
    public class AccountController : BaseController
    {
        private readonly userNS.AuthenticationService _authenticationService;
        private LoginSessionService _loginSessionService;

        public AccountController(userNS.AuthenticationService authenticationService, LoginSessionService loginSessionService) : base()
        {
            _authenticationService = authenticationService;
            _loginSessionService = loginSessionService;
        }
        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> login(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var authenticationDetail = _authenticationService.validateUser(model.username, model.password);

                    if (authenticationDetail == null)
                    {
                        throw new Exception("Username and password didnot match.");
                    }

                    var claims = new List<Claim>()
                    {
              new Claim(ClaimTypes.NameIdentifier,authenticationDetail.authentication_id.ToString())
                   };

                    var userIdentity = new ClaimsIdentity(claims, "local");

                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    AuthenticationProperties prop = new AuthenticationProperties();
                    prop.ExpiresUtc = DateTime.UtcNow.AddDays(30);
                    prop.IsPersistent = model.remember_me;
                    await HttpContext.SignInAsync("userDetails", principal, prop);

                    var sessionDto =new CMS.User.Dto.LoginSessionDto()
                    {
                        authentication_id = authenticationDetail.authentication_id,
                        type = CMS.User.Enums.SessionType.login
                    };
                    _loginSessionService.save(sessionDto);
                    return Redirect("/admin");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(model);
        }

        public async Task<IActionResult> logout()
        {
            var authenticationId = getLoggedInAuthenticationId();
            await HttpContext.SignOutAsync();

            var sessionDto = new CMS.User.Dto.LoginSessionDto()
            {
                authentication_id = authenticationId,
                type = CMS.User.Enums.SessionType.logout
            };
            _loginSessionService.save(sessionDto);

            return Redirect("/account/login");
        }
    }
}