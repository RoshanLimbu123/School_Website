using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface VideoRepository
    {
        void update(Video video);
        void delete(Video video);
        void insert(Video video);
        List<Video> getAll();
        Video getById(long video_id);
        IQueryable<Video> getQueryable();
        Video getHomeVideo();

    }
}
