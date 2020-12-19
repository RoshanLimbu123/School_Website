using CMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.User_management.FilterModel
{
    public class UserFilter:PaginationFilter
    {
        public string name { get; set; }
    }
}
