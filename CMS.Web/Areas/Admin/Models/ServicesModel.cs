using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.Models
{
    public class ServicesModel
    {
        public long service_id { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; }
        [Display(Name = "Description")]

        public string description { get; set; }
       
        [Display(Name = "Image")]
        public string image { get; set; }
        public bool is_active { get; set; } = true;
    }
}
