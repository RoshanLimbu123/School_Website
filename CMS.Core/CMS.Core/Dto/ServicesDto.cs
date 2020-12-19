using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Dto
{
    public class ServicesDto
    {
        [key]
        public long service_id { get; set; }

        [Display(Name="Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required.")]
        public string name { get; set; }

        [Display(Name = "Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required.")]
        public string description { get; set; }
     
        public string slug { get; set; }

        [Display(Name ="Image")]
        public string image { get; set; }
        public bool is_active { get; set; } = true;
    }
}
