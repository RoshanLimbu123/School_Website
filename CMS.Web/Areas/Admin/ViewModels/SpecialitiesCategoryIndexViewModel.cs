using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.ViewModels
{
    public class SpecialitiesCategoryIndexViewModel
    {
        public List<specialitiesCategoryDetailModel> specialitiesCategoryDetailModels { get; set; }
    }

    public class specialitiesCategoryDetailModel
    {


        public long specialities_category_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public string image_name { get; set; }
        public bool is_enabled { get; set; }
        // public Page pages { get; set; }
    }
}
