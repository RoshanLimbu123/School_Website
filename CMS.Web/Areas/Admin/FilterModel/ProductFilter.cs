using CMS.Web.Areas.Core.Enums;
using CMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.FilterModel
{
    public class ProductFilter:PaginationFilter
    {
        public string title { get; set; }
        public long item_category_id { get; set; }
        public StatusFilter status { get; set; }
    }
}
