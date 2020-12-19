using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using CMS.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class GalleryImageMakerImpl : GalleryImageMaker
    {
        public void copy(GalleryImage gallery, GalleryImageDto gallery_dto)
        {
            if (gallery_dto.image_name != null)
            {
                gallery.image_name = gallery_dto.image_name;
            }
            gallery.gallery_id = gallery_dto.gallery_id;
            gallery.title = gallery_dto.title;
            gallery.description = gallery_dto.description;
            gallery.is_enabled = gallery_dto.is_enabled;
            gallery.is_slider_image = gallery_dto.is_slider_image;
            gallery.is_default = gallery_dto.is_default;
        }
    }
}
