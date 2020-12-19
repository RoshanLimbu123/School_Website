using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.Models
{
    public class PageCategoryModel
    {
        public long page_category_id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Page Category name is required")]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Status")]
        public bool is_enabled { get; set; }
    }
}
