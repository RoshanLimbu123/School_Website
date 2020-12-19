using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entity
{
    public class ItemCategory
    {
        private string _name;
        [Key]
        public long item_category_id { get; set; }

        [Required]
        [MaxLength(50)]
        public string name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Item Category Name is required.");
                }
                _name = value;
            }
        }

        [Required]
        [MaxLength(55)]
        public string slug { get; set; }

        [MaxLength(2000)]
        public string description { get; set; }

        public bool is_enabled { get; set; } = true;

        public virtual List<Product> products { get; set; }
        public virtual List<Teacher> teachers { get; set; }

        public bool hasProducts() => products.Count > 0;
        public bool hasTeachers() => teachers.Count > 0;

        public void enable()
        {
            this.is_enabled = true;
            products.ForEach(c => c.is_enabled = true);
            teachers.ForEach(c => c.is_enabled = true);
        }

        public void disable()
        {
            this.is_enabled = false;
            products.ForEach(c => c.is_enabled = false);
            teachers.ForEach(c => c.is_enabled = false);
        }
    }

}
