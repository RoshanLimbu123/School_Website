using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.ViewModels
{
    public class PageViewModel
    {
        public List<PageDetail> products { get; set; }
      
         
    }
    public class PageDetail
    {
        public long page_id { get; set; }
        public long page_category_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string image_name { get; set; }
    }
}
