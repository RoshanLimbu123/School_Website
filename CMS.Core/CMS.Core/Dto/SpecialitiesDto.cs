using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Dto
{
    public class SpecialitiesDto
    {
        private string _title;
        public long specialities_id { get; set; }

        [Display(Name = "Specialities Category")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please choose a category")]
        public long specialities_category_id { get; set; }

        [Display(Name = "Title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a title")]
        [MaxLength(60,ErrorMessage ="Title cannot be more than 60 letters.")]
        public string title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Title is required.");
                }
                _title = value;
            }
        }

        [Display(Name = "Description")]
        public string description { get; set; }
        [Display(Name = "Image")]

        public string image_name { get; set; }

        [Display(Name ="Status")]
        public bool is_enabled { get; set; } = true;
    }
}
