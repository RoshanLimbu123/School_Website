using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.ViewModels
{
    public class GalleryImageIndexViewModel
    {
        public List<GalleryImageDetailModel> gellery_details { get; set; }
    }

    public class GalleryImageDetailModel
    {
        public long gallery_image_id { get; set; }

        public string title { get; set; }
        public virtual Gallery gallery { get; set; }
        public string description { get; set; }
        public string image_name { get; set; }
        public bool is_slider_image { get; set; }
        public bool is_default { get; set; }
        public bool is_enabled { get; set; }
    }
}
