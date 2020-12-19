using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entity
{
    public class PageCategory
    {
        private string _name;

        [Key]
        public long page_category_id { get; set; }

        [Required]
        [MaxLength(60)]
        public string name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exceptions.NonNullValueException("Page category name is required.");
                }
                _name = value;
            }
        }

        [Required]
        [MaxLength(70)]
        public string slug { get; set; }
        public bool is_enabled { get; set; } = true;

        public virtual List<Page> pages { get; set; }

        public bool hasPages() => pages.Count>0;

        public void enable()
        {
            this.is_enabled = true;
            pages.ForEach(c => c.is_enabled = true);
        }

        public void disable()
        {
            this.is_enabled = false;
            pages.ForEach(c => c.is_enabled = false);
        }

    }
}
