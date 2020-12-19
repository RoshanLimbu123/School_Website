using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.ViewModels
{
    public class PageIndexViewModel
    {
        public List<PageDetailModel> page_details { get; set; }
    }

    public class PageDetailModel
    {
        public long page_id { get; set; }
        public long page_category_id { get; set; }
        public string page_category_name { get; set; }

        public string title { get; set; }
        public string description { get; set; }
        public string image_name { get; set; }
       
        public bool is_enabled { get; set; }
        public bool is_home_page { get; set; }
    }
}
