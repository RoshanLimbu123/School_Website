using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entity
{
    public class Commerce
    {
        [Key]
        public long commerce_id { get; set; }
        [Required]
        [MaxLength(250)]
       
        public string description { get; set; }
        [Required]
        [MaxLength(120)]
        public string slug { get; set; }
        [MaxLength(70)]
        public string image_name { get; set; }
        private string _title { get; set; }
        public string title
        {
            get => _title;
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ItemNotFoundException("Title is Required");
                }
                _title = value;
            }
        }
        public bool is_enabled { get; set; } = true;
        public void enable()
        {
            is_enabled= true;
        }
        public void disable()
        {
         is_enabled= false;
        }
    }
}
