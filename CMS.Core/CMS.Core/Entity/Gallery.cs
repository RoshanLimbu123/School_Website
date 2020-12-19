using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Core.Entity
{
   public class Gallery
    {
        [Key]
        public long gallery_id { get; set; }

        public bool is_active { get; set; } = true;
        public string _description { get; set; }
        public void active()
        {
            is_active = true;
        }
        public void inactive()
        {
            is_active = false;
        }
        private string _name;
        [Required]
        [MaxLength(200)]
        public string name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("name is required");
                }
                _name = value;
            }
        }
          
        public string description { get; set; }

        public virtual List<GalleryImage> gallery_images { get; set; }

    }
}
