using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface GalleryImageService
    {
        void save(GalleryImageDto gallery_dto);
        void update(GalleryImageDto gallery_dto);
        void delete(long gallery_image_id);
        void enable(long gallery_image_id);
        void disable(long gallery_image_id);
        void custom(long gallery_image_id);
        void default1(long gallery_image_id);
        void makeSliderImage(long gallery_image_id);
        void removeFromSliderImage(long gallery_image_id);
    }
}
