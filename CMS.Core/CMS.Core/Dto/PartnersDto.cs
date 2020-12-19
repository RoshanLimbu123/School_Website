using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Dto
{
    public class PartnersDto
    {
        [Key]

        public long partners_id { get; set; }
        [Display(Name = "Name")]
        public string name { get; set; }
        [Display(Name = "Logo")]
        public string logo_name { get; set; }
        [Display(Name="Web Url")]
        public string web_url { get; set; }

        public bool is_active { get; set; } = true;

    }
}
