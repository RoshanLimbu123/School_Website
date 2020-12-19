using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface GalleryRepository
    {
        void insert(Gallery gallery_id);
        void update(Gallery gallery_id);
        void delete(Gallery gallery_id);
        Gallery getById(long gallery_id);
        List<Gallery> getAll();
        IQueryable<Gallery> getQueryable();
    }
}
