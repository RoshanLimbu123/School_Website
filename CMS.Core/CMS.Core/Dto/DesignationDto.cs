using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Dto
{
    public class DesignationDto
    {
        private string _name,_position;


        [Key]
        public long Designation_id { get; set; }
        [Display(Name ="Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Name")]
        public string name {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Name is Required.");
                }
                _name = value;
            }
        }
        [Display(Name = "Position")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter position")]
        public string position
        {
            get => _position;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Position is Required.");
                }
                _position = value;
            }
        }
    }
}
