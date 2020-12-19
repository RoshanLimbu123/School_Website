using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Dto
{
    public class CareerDto
    {
        private string _title, _description;
        
        public long career_id { get; set; }

        [Display(Name ="Title")]
        [Required(AllowEmptyStrings =false,ErrorMessage ="Please enter a title.")]
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

        [Display(Name = "Opening Date")]
        public DateTime opening_date { get; set; } = DateTime.Now;
        [Display(Name = "Closing Date")]
        public DateTime? closing_date { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }
      
        [Display(Name ="Image")]
        public string image_name { get; set; }

        [Display(Name = "Status")]
        public bool is_closed { get; set; } = false;
    }
}
