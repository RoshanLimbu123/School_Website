using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entity
{
    public class Setup
    {
        private string _key, _value;

        [Key]
        public long setup_id { get; set; }
        [Required]
        [MaxLength(70)]
        public string key
        {
            get => _key;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Key is required.");
                }
                _key = value;
            }
        }

        [Required]
        [MaxLength(500)]
        public string value { get; set; }
    }
}
