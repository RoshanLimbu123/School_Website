using CMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.FilterModel
{
    public class BlogFilter : PaginationFilter
    {
        public string title { get; set; }
    }
}
