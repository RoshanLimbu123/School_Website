using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.Models
{
    public class SpecialitiesCategoryModel
    {
        public long specialities_category_id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Specialities Category name is required")]
        [Display(Name = "Name")]
        public string name { get; set; }
        [Display(Name = "Description")]

        public string description { get; set; }

        [Display(Name = "Image Name")]
        public string image_name { get; set; }

        [Display(Name = "Status")]
        public bool is_enabled { get; set; }
    }
}
