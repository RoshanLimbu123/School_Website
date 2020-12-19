using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entity
{
    public class Partners
    {
        private string _name;
        [Key]

        public long partners_id { get; set; }
        [Required]
        [MaxLength(250)]
        public string name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Name is Required");
                }
                _name = value;

            }
        }
        [MaxLength(250)]

        public string logo_name { get; set; }
        public string web_url { get; set; }
        public bool is_active { get; set; } = true;
        public void active()
        {
            is_active = true;
        }
        public void inactive()
        {
            is_active = false;
        }
    }
}
