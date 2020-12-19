using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.ViewModels
{
    public class PageCategoryIndexViewModel
    {
        public List<PageCategoryDetailModel> page_category_details { get; set; }
    }

    public class PageCategoryDetailModel
    {
        public long page_category_id { get; set; }
        public string name { get; set; }
        public bool is_enabled { get; set; }
       // public Page pages { get; set; }
    }
}
