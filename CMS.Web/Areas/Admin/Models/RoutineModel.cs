using CMS.Core.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.Models
{
    public class RoutineModel
    {


        public long routine_id { get; set; }
        [ForeignKey("fiscal_year_id")]
        public long fiscal_year_id { get; set; }
        public virtual FiscalYear fiscalYear { get; set; }
        [ForeignKey("class_id")]

        public long class_id { get; set; }
        public virtual Classes classes { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Start Date is required")]
        [Display(Name = " Start Date")]
        public DateTime start_date { get; set; } = DateTime.Now;
        [Display(Name = "End Date")]
        public DateTime end_date { get; set; }
        [Display(Name = "Image")]
        public string image { get; set; }
        [Display(Name = "Status")]
        public bool is_active { get; set; } = true;
        public IEnumerable<SelectListItem> Classes { get; set; }


    }
}
