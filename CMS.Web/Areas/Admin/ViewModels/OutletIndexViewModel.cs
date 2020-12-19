using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.ViewModels
{
    public class OutletIndexViewModel
    {
        public List<OutletDetailModel> outlet_details { get; set; }
    }

    public class OutletDetailModel
    {
        public long outlet_id { get; set; }

        public string title { get; set; }
        public string description { get; set; }
        public string address { get; set; }
        public string file_name { get; set; }
        public bool is_enabled { get; set; }
    }
}
