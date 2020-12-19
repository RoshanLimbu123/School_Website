using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.Models
{
    public class GalleryImageModel
    {
        public long gallery_image_id { get; set; }

        [Display(Name = "Title")]
        public string title { get; set; }
        [ForeignKey("gallery_id")]
        public long gallery_id { get; set; }
        public virtual Gallery gallery { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Image")]
        public string image_name { get; set; }

        [Display(Name = "Is Slider")]
        public bool is_slider_image { get; set; }

        [Display(Name = "Status")]
        public bool is_enabled { get; set; } = true;
        public bool is_default { get; set; }
    }
}
