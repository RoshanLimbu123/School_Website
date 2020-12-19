using CMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Admin.FilterModel
{
    public class EventFilter : PaginationFilter
    {
        public string title { get; set; }
        public DateTime from_date { get; set; } = DateTime.Now.AddMonths(-1);
        public DateTime to_date { get; set; } = DateTime.Now.AddMonths(1);
    }
}
