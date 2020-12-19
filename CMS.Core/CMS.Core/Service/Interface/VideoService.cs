using CMS.Core.Dto;
using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface VideoService
    {
        void save(VideoDto videoDto );
        void update(VideoDto videoDto);
        void delete(long video_id);
        void enable(long video_id);
        void disable(long video_id);
        void makeHomeVideo(long video_id);

    }
}
