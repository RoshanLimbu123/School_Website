using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.User_management.Models
{
    public class PermissionModel
    {
        public string permission_name { get; set; }
        public bool is_checked { get; set; }
    }
}
