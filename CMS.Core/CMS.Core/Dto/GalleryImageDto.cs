using CMS.Core.Entity;
using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Core.Dto
{
    public class GalleryImageDto
    {
        private string _title, _imageName;


        [Key]
        public long gallery_image_id { get; set; }

        public long gallery_id { get; set; }

        [Display(Name = "Image")]
        public string image_name
        {
            get => _imageName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Image is required.");
                }
                _imageName = value;
            }
        }
        [Display(Name = "Title")]
        public string title { get; set; }
        [Display(Name = "Description")]
        public string description { get; set; }
       
        public bool is_enabled { get; set; } = true;

        public bool is_slider_image { get; set; } = false;

    
        public bool is_default { get; set; } = false;
   
    }
}