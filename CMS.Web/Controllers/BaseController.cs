using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CMS.Core.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers
{
    public class BaseController : Controller
    {

        protected long getLoggedInUserId()
        {
            return 1;
        }

        protected long getLoggedInAuthenticationId()
        {
            try
            {
                string loggedInAuthenticationId = Request.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(loggedInAuthenticationId))
                {
                    return 0;
                }
                return Convert.ToInt64(loggedInAuthenticationId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }
    }
}