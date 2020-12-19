using CMS.Core.Dto;
using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.ViewModels
{
    public class GalleryViewModel
    {
        public List<GalleryDetails> gallery { get; set; }
 
    }
    public class GalleryDetails
    {

       public long gallery_id { get; set; }
        public bool is_active { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public virtual List<GalleryImage> gallery_images { get; set; }

    }
}
   
