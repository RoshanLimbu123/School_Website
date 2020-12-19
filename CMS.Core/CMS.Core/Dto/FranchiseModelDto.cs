using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Dto
{
   public class FranchiseModelDto
    {
        private string _title, _descriptionz;

        public long franchise_model_id { get; set; }

        [Display(Name = "Title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required.")]
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
        [Required]
        [MaxLength(60)]
        public string slug { get; set; }
        public string file_name { get; set; }
        [Display(Name = "Status")]
        public bool is_enabled { get; set; } = true;
    }
}

