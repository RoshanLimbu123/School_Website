using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.ViewModels
{
    public class SpecialitiesViewModel
    {
        public List<SpecialitiesDetails>  specialities_details { get; set; }

    }

    public class SpecialitiesDetails
    {
        public long specialities_id { get; set; }
        public long specialities_category_id { get; set; }
        public string specialities_category_name { get; set; }

        public string title { get; set; }
        public string description { get; set; }
        public string image_name { get; set; }
        public string slug { get; set; }
        public bool is_enabled { get; set; }
        public virtual SpecialitiesCategory specialitiescategory { get; set; }
    }
    
}

