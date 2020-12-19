using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entity
{
   public class Enquiry
    {
        private string _name;
        private string _email;

        [Key]
        public long enquiry_id { get; set; }
        [MaxLength(100)]
        public string address
        {
            get; set;
        }
        [MaxLength(50)]
        public string contact_number { get; set; }


        [Required]
        [MaxLength(200)]
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
        [MaxLength(300)]
        public string email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Email is Required.");
                }
                _email = value;
            }
        }


        [MaxLength(5000)]
        public string description { get; set; }

       
        public DateTime enquiry_date { get; set; } = DateTime.Now;
    }

}

