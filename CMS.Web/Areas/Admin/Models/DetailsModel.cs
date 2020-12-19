using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.Models
{
    public class DetailsModel
    {
        public long details_id { get; set; }
        [Display(Name = "value1")]
        public long value1 { get; set; }
        [Display(Name = "value2")]

        public long value2 { get; set; }
        [Display(Name = "value3")]
        public long value3 { get; set; }
        [Display(Name = "value4")]
        public long value4 { get; set; }
        [Display(Name = "value5")]
        public long value0 { get; set; }
    }
}
