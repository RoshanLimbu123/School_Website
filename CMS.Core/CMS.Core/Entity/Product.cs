using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Core.Entity
{
    public class Product
    {
        private string _name;

        [Key]
        public long product_id { get; set; }

        [Required]
        public long item_category_id { get; set; }

        [Required]
        [MaxLength(100)]
        public string name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exceptions.NonEmptyValueException("Product name is required.");
                }
                _name = value;
            }
        }

        [Required]
        [MaxLength(120)]
        public string slug { get; set; }


        public string description { get; set; }

        public string specification { get; set; }

        public string features { get; set; }

        public bool is_enabled { get; set; } = true;

        [MaxLength(110)]
        public string file_name { get; set; }

        [ForeignKey("item_category_id")]
        public virtual ItemCategory item_category { get; set; }

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
