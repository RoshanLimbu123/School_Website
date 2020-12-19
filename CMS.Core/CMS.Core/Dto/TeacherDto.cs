using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Dto

{
   public class TeacherDto
    {
        private string _name;
        public long teacher_id { get; set; }
        [Display]
        [Required(AllowEmptyStrings =false,ErrorMessage ="Please Enter Name ")]
        public string name
        { 
            get => _name;
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Name is Required");
                }
                _name = value;
            }
        }
        [Display(Name ="Item Category")]
        [Required(AllowEmptyStrings =false,ErrorMessage ="Please choose an item Category")]
        public long item_category_id { get; set; }
        [Display(Name ="Description")]
        public string description { get; set; }
        [Display(Name ="Image")]
        public string file_name { get; set; }
        [Display(Name ="Status")]
        public bool is_enabled { get; set; } = true;
    }
}
