using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.ViewModels
{
    public class GalleryIndexViewModel
    {
        public List<GalleryDetailModel> gallery_details { get; set; }
    }
    public class GalleryDetailModel
    {


        public long gallery_id { get; set; }

        public bool is_active { get; set; } = true;
        public void active()
        {
            is_active = true;
        }
        public void inactive()
        {
            is_active = false;
        }


        public string name { get; set; }


        public string description { get; set; }

        public virtual List<GalleryImage> gallery_images { get; set; }
    }
}
