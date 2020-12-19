
using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Core.Entity
{
    public class Member
    {
        private string _full_name;

        [Key]
        public long member_id { get; set; }


        [ForeignKey("Designation_id")]
        public virtual Designation Designation { get; set; }

        public long Designation_id { get; set; }

        [ForeignKey("fiscal_year_id")]
        public virtual FiscalYear fiscalyear { get; set; }

        public long fiscal_year_id { get; set; }
        [Required]
        [MaxLength(50)]
        public string full_name
        {
            get => _full_name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Full Name is Required.");
                }
                _full_name = value;
            }
        }

        [MaxLength(50)]
        public string address
        {
            get; set;
        }
        [MaxLength(50)]
        public string contact_number { get; set; }

        [MaxLength(120)]
        public string image_url { get; set; }
        public bool is_contact_enabled { get; set; } = false;
   
        [MaxLength(2000)]
        public string description { get; set; }

        public void enable()
        {
            is_contact_enabled = true;
        }

        public void disable()
        {
            is_contact_enabled = false;
        }
    }
}



