using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entity
{
   public class Video
    {
        private string _video_url;

        [Key]
        public long video_id { get; set; }

        [Required]
        [MaxLength(200)]
        public string video_url
        {
            get => _video_url;
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Video url is required");
                }
                _video_url = value;
            }
        }
        [MaxLength(50000)]
        public string description { get; set; }
        public string title { get; set; }
        public bool is_enabled { get; set; } = true;
        public void enable()
        {
            is_enabled = true;
        }

        public void disable()
        {
            is_enabled = false;
        }

        public bool is_home_video { get; set; } = false;
        public void makeHomePage()
        {
            is_home_video = true;
        }
        public void unmakeHomePage()
        {
            is_home_video = false;
        }
    }
}
