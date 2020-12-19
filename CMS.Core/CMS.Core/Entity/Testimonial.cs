using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entity
{
    public class Testimonial
    {
        private string _personName, _statement, _designation, _associatedCompanyName;

        [Key]
        public long testimonial_id { get; set; }

        [Required]
        [MaxLength(50)]
        public string person_name
        {
            get => _personName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exceptions.NonEmptyValueException("Person name cannot be empty.");
                }
                _personName = value;
            }
        }

        [Required]
        [MaxLength(500)]
        public string statement
        {
            get => _statement;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exceptions.NonEmptyValueException("Statement cannot be empty.");
                }
                _statement = value;
            }
        }

        [Required]
        [MaxLength(50)]
        public string designation
        {
            get => _designation;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exceptions.NonEmptyValueException("Designation cannot be empty.");
                }
                _designation = value;
            }
        }

        [Required]
        [MaxLength(200)]
        public string associated_company_name
        {
            get => _associatedCompanyName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exceptions.NonEmptyValueException("Associated company name cannot be empty.");
                }
                _associatedCompanyName = value;
            }
        }

        [MaxLength(100)]
        public string image_name { get; set; }

        public bool is_visible { get; set; } = true;

        public void makeVisible()
        {
            is_visible = true;
        }

        public void makeInvisible()
        {
            is_visible = false;
        }
    }
}
