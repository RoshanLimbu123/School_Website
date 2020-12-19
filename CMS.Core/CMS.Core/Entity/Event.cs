using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entity
{
    public class Event
    {
        private string _title;
        private string _description;
        private string _venue;
        [Key]
        public long event_id { get; set; }
        [Required]
        [MaxLength(250)]
        public string title
        {
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
        [MaxLength(120)]
        public string slug { get; set; }

        [Required]
        public DateTime event_from_date { get; set; } = DateTime.Now;
        public DateTime event_to_date { get; set; } = DateTime.Now.AddDays(7);


        [Required]
        public string description
        {
            get => _description;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Description is required.");
                }
                _description = value;
            }
        }

        [MaxLength(70)]
        public string image_name { get; set; }

        [Required]
        [MaxLength(250)]
        public string venue
        {
            get => _venue;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Venue is required.");
                }
                _venue = value;
            }
        }
        [MaxLength(70)]
        public string file_name { get; set; }

        [MaxLength(200)]
        public string time { get; set; }

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
