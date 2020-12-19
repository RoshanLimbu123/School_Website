using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entity
{
    public class Notice
    {
        private string _title;

        [Key]
        public long notice_id { get; set; }
        [Required]
        public DateTime notice_date { get; set; } = DateTime.Now;
        public DateTime notice_expiry_date { get; set; } = DateTime.Now.AddDays(7);

      
        public string description { get; set; }
      

        [Required]
        [MaxLength(120)]
        public string slug { get; set; }

        [MaxLength(70)]
        public string image_name { get; set; }

        [Required]
        [MaxLength(250)]
        public string title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Title is required.");
                }
                _title = value;
            }
        }
        public bool is_closed { get; set; } = false;

        public void close()
        {
            is_closed = true;
        }

        public void unclose()
        {
            is_closed = false;
        }
    }
}
