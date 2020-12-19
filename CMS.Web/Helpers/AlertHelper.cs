using CMS.Web.Areas.Admin.Controllers;
using CMS.Web.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Helpers
{
    public class AlertHelper
    {
        public static void setMessage(Controller controller, string message, messageType message_type =messageType.success)
        {
            Alert alert = new Alert();
            alert.message = message;
            alert.message_type = message_type;
            controller.TempData["message"] = JsonConvert.SerializeObject(alert);
        }

        internal static void setMessage(HeaderViewComponent headerViewComponent, string v, messageType success)
        {
            throw new NotImplementedException();
        }
    }
}
