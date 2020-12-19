using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entity
{
    public class Designation
    {
        private string _name;
        private string _position;
        [Key]
        public long Designation_id { get; set; }

        [Required]
        [MaxLength(50)]
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
        [Required]
        [MaxLength(50)]
        public string position {
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
