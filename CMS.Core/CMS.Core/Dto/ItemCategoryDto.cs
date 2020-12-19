using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Dto
{
    public class ItemCategoryDto
    {
        private string _name;

        public long item_category_id { get; set; }

        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter an item category name")]
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
        

        [Display(Name ="Description")]
        public string description { get; set; }

        [Display(Name ="Status")]
        public bool is_enabled { get; set; } = true;

        public List<ProductDto> products { get; set; }
    }
}
