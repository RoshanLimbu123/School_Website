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
    public class VideoModel
    {
        public long video_id { get; set; }

        [Display(Name = "Title")]
        public string title { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "VIDEO URL")]
        public string video_url { get; set; }

        [Display(Name = "Status")]
        public bool is_enabled { get; set; } = true;
    }
}
