using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.Models
{
    public class SpecialitiesModel
    {
        public long specialities_id { get; set; }
        [Display(Name ="Category")]
        public long specialities_category_id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Specialities title is required")]
        [Display(Name ="Title")]
        public string title { get; set; }
        [Display(Name ="Description")]
        public string description { get; set; }
        [Display(Name ="Image")]
        public string image_name { get; set; }

        [Display(Name = "Status")]
        public bool is_enabled { get; set; } = true;
    }
}
