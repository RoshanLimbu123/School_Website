using CMS.Core.Entity;
using CMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Admin.FilterModel
{
    public class MembersFilter : PaginationFilter
    {
        public string title { get; set; }
    }
}
