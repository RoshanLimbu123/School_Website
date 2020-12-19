using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.ViewModels
{
    public class SpecialitiesIndexViewModel
    {
        public List<SpecialitiesDetailModel> specialities_details { get; set; }
    }

    public class SpecialitiesDetailModel
    {
        public long specialities_id { get; set; }
        public long specialities_category_id { get; set; }
        public string specialities_category_name { get; set; }

        public string title { get; set; }
        public string description { get; set; }
        public string image_name { get; set; }
       
        public bool is_enabled { get; set; }
    }
}
