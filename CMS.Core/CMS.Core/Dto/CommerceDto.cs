using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Dto
{
  public  class CommerceDto
    {
        private string _title, _description;
     public long commerce_id { get; set; }
       [Display(Name ="Title")]
       [Required(AllowEmptyStrings =false,ErrorMessage ="Please enter title")]
        public string title
        {
            get => _title;
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ItemNotFoundException("Title is Required");
                }
                _title = value;
            }
        }
        [Display(Name ="Description")]
        public string description { get; set; }
        [Display(Name ="Image")]
        public string image_name { get; set; }
        public bool is_enabled { get; set; } = true;
    }
}
