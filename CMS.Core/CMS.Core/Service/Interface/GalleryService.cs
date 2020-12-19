using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Repository.Interface
{
   public interface GalleryService
    {
        void delete(long gallery_id);
        void save(GalleryDto galleryDto);
        void update(GalleryDto galleryDto);
       
        void active(long gallery_id);
        void inactive(long gallery_id);
    }
}
