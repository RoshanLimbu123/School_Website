using CMS.Core.Dto;
using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.ViewModels
{
    public class GalleryImageViewModel
    {

        public List<GalleryImageDetail> gellery_details { get; set; }
     
    }

    public class GalleryImageDetail
    {
        public long gallery_image_id { get; set; }
        public long gallery_id { get; set; }
        public string title { get; set; }
        public Gallery gallery { get; set; }
        public string description { get; set; }
        public string image_name { get; set; }
        public bool is_slider_image { get; set; }
        public bool is_default { get; set; }
        public bool is_enabled { get; set; }
        public string slug { get; set; }
        public string gallery_name { get; set; }

    }
}

