using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.ViewModels
{
    public class VideoModel
    {
        public List<VideoModalDetails> videodetails { get; set; }

    }
    public class VideoModalDetails
    {
        public long video_id { get; set; }

        public string title { get; set; }
        public string description { get; set; }
        public string video_url { get; set; }
        public bool is_enabled { get; set; } = true;
    }
}
