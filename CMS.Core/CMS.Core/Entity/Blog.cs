using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Core.Entity
{
    public class Blog
    {
        private string _title;
        private string _artical_by;
        private string _description;
        [Key]
        public long blog_id { get; set; }

        [ForeignKey("blogComment")]
        public virtual List<BlogComment> blogComment { get; set; }

        [Required]
        [MaxLength(120)]
        public string slug { get; set; }


        [Required]
        [MaxLength(150)]
        public string title {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Title is Required.");
                }
                _title = value;
            }
        }
        [Required]
        public DateTime posted_on { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(200)]
        public string artical_by { get => _artical_by;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Artical By is Required.");
                }
                _artical_by = value;
            }
        }
    
        [MaxLength(50000)]
        public string description { get; set; }
        [MaxLength(100)]
        public string image_name { get; set; }

        
        public bool is_enabled { get; set; } = true;

        public void enable()
        {
            is_enabled = true;
        }

        public void disable()
        {
            is_enabled = false;
        }
    }
}
