using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entity
{
    public class Career
    {
        private string _title, _description;

        [Key]
        public long career_id { get; set; }
        [Required]
        [MaxLength(50)]
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

        [Required]
        public DateTime opening_date { get; set; } = DateTime.Now;
        public DateTime? closing_date { get; set; }

       
        [MaxLength(2000)]
        public string description { get; set; }
       
        [MaxLength(70)]
        public string image_name { get; set; }

        public bool is_closed { get; set; } = false;
        [Required]
        [MaxLength(120)]
        public string slug { get; set; }

        public void markClosed()
        {
            is_closed = true;
            closing_date = DateTime.Now;
        }

        public void markUnclosed()
        {
            is_closed = false;
            closing_date = null;
        }
    }
}
