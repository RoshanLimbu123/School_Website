using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Admin.ViewModels
{
    public class HomeIndexViewModel
    {
        public int active_careers_count { get; set; }
        public int active_products_count { get; set; }
        public int active_notices_count { get; set; }
        public int pages_count { get; set; }
        public string company_name { get; set; }
        public string address { get; set; }

    }
}
