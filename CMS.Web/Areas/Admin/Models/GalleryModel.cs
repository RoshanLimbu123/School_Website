using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.Models
{
    public class GalleryModel
    {
        public long gallery_id { get; set; }
        [Display(Name = "Status")]
        public bool is_active { get; set; }
       
         [Display(Name = "Description")]
        public string description { get; set; }
        [Display(Name = "Gallery Name")]
        public string name { get; set; }
        public List<Gallery> gallery { get; set; }
    }
}
