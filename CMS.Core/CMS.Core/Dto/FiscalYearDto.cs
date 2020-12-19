
using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Dto
{
    public class FiscalYearDto
    {

        private string _name;

        [Key]
        public long fiscal_year_id { get; set; }
        [Display(Name ="Name")]

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Name")]
        public string name
        {
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
        [Required]
        public bool is_current { get; set; }
    }
}
