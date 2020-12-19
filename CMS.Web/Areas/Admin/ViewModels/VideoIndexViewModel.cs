using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Admin.ViewModels
{
    public class VideoIndexViewModel
    {
        public List<VideoDetails> videodetails { get; set; }

    }
    public class VideoDetails
    {
        public long video_id { get; set; }

        public string title { get; set; }
        public string description { get; set; }
        public string video_url { get; set; }
        public bool is_enabled { get; set; } = true;
        public bool is_home_video { get; set; }
    }
}
