using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Core.Entity
{
 public   class Teacher
    {
        [Key]
        public long teacher_id { get; set; }
        private string _name;
        [Required]
        public long item_category_id { get; set; }
        [Required]
        [MaxLength(100)]
        public string name
        {
            get => _name;
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Teacher Name is Required");
                }
                _name = value;
            }
        }
        [ForeignKey("item_category_id")]
        public virtual ItemCategory item_category { get; set; }
        [MaxLength(110)]
        public string file_name { get; set; }
        [Required]
        [MaxLength(120)]
        public string slug { get; set; }
        public string description { get; set; }
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
