using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.Models
{
    public class FranchiseModels
    {
        public long franchise_model_id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Outlet title is required")]
        [Display(Name ="Title")]
        public string title { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

       

        [Display(Name = "File")]
        public string file_name { get; set; }

        [Display(Name = "Status")]
        public bool is_enabled { get; set; } = true;
    }
}
