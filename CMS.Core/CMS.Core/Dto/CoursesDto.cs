using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Dto
{
    public class CoursesDto
    {
        private string _name;

        public long course_id { get; set; }

        [Display(Name = "Faculty")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please choose a Faculty")]
        public long faculty_id { get; set; }

        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a name")]
        public string name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exceptions.NonEmptyValueException("Courses name is required.");
                }
                _name = value;
            }
        }

        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Specification")]
        public string specification { get; set; }

        [Display(Name = "Features")]
        public string features { get; set; }

        [Display(Name ="Status")]
        public bool is_enabled { get; set; } = true;

        [Display(Name ="File")]
        public string file_name { get; set; }
    }
}
