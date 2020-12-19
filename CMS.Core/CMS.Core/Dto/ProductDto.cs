using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Dto
{
    public class ProductDto
    {
        private string _name;

        public long product_id { get; set; }

        [Display(Name = "Item Category")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please choose an item category")]
        public long item_category_id { get; set; }

        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a name")]
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
       

        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Details URL")]
        public string specification { get; set; }

        [Display(Name = "Features")]
        public string features { get; set; }

        [Display(Name ="Status")]
        public bool is_enabled { get; set; } = true;
        [Display(Name = "Image")]
        public string file_name { get; set; }
    }
}
