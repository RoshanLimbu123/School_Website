using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Dto
{
    public class SpecialitiesCategoryDto
    {
        private string _name;
        public long specialities_category_id { get; set; }

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
                    throw new Exceptions.NonNullValueException("Specialities category name is required.");
                }
                _name = value;
            }
        }
        [Display(Name = "Description")]

        public string description { get; set; }

        [MaxLength(70)]
        [Display(Name = "Image")]

        public string image_name { get; set; }
        [Display(Name ="Status")]
        public bool is_enabled { get; set; }
        public List<SpecialitiesDto> SpecialitiesDtos { get; set; }
    }
}
