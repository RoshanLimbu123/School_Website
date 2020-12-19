using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Dto
{
    public class PageCategoryDto
    {
        private string _name;
        public long page_category_id { get; set; }

        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter an item category name")]
        [MaxLength(60, ErrorMessage = "Name cannot be more than 60 letters.")]
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

        [Display(Name ="Status")]
        public bool is_enabled { get; set; }
        public List<PageDto> pages { get; set; }
    }
}
