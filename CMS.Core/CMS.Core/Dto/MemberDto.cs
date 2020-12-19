using CMS.Core.Entity;
using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Core.Dto
{
    public class MemberDto
    {
        private string _full_name;


        public long member_id { get; set; }

        [Display(Name="Designation")]
        public long Designation_id { get; set; }
        [Display(Name="Fiscal Year")]
        public long fiscal_year_id { get; set; }


        [Display(Name = "Fiscal Year")]
        public virtual FiscalYear fiscalyear { get; set; }

        [MaxLength(2000)]
        public string description { get; set; }

        [Display(Name = "Full Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a Full Name")]
        public string full_name
        {
            get => _full_name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Full Name is required.");
                }
                _full_name = value;
            }
        }
        [Display(Name = "Address")]
        public string address { get; set; }

        [Display(Name = "Contact Number")]
        public string contact_number { get; set; }

        [Display(Name ="Image")]
        [MaxLength(120)]
        public string image_url { get; set; }
        [Display(Name = "Enable Contact No ?")]
        public bool is_contact_enabled { get; set; } = false;
    }
}
