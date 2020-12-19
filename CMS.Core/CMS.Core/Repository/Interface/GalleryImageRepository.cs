using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface GalleryImageRepository
    {
        void insert(GalleryImage gallery);
        void update(GalleryImage gallery);
        void delete(GalleryImage gallery);
        List<GalleryImage> getAll();
        GalleryImage getById(long gallery_image_id);
        GalleryImage getByName(string name);
        IQueryable<GalleryImage> getQueryable();
    }
}
