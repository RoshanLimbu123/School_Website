using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace CMS.Core.Makers.Implementations
{
    public class VideoMakerImpl : VideoMaker
    {
        public void copy(Video video, VideoDto videoDto)
        {
            video.video_id = videoDto.video_id;
            video.description = videoDto.description;
            video.title = videoDto.title;
            video.is_enabled = videoDto.is_enabled;
            video.video_url = videoUrl(videoDto.video_url);
        }

        private string videoUrl(string dto)
        {
            var uri = new Uri(dto);
            var query = HttpUtility.ParseQueryString(uri.Query);
            var videoId = query["v"];
            return videoId;

        }


    }
}
