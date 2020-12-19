using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Core.Entity
{
    public class GalleryImage
    {
        private string _imageName;

       
        [Key]
        public long gallery_image_id { get; set; }
       

        public long gallery_id { get; set; }

        [ForeignKey("gallery_id")]

        public virtual Gallery gallery { get; set; }

        [Required]
        [MaxLength(70)]
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

        [MaxLength(70)]
        public string title { get; set; }
        public string description { get; set; }

        public bool is_enabled { get; set; } = true;

        public bool is_slider_image { get; set; } = false;

        public void enable()
        {
            is_enabled = true;
        }

        public void disable()
        {
            is_enabled = false;
        }

        public void markSliderImage()
        {
            is_slider_image = true;
        }

        public void removeFromSliderImage()
        {
            is_slider_image = false;
        }
        public bool is_default { get; set; } = false;
        public void default1()
        {
            is_default = true;
        }
        public void custom()
        {
            is_default = false;
        }
    }
}

