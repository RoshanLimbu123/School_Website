using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entity
{
   public class Services
    {
        private string _name,_description;
        [Key]
        public long service_id { get; set; }
        [Required]
        [MaxLength(250)]
        public string name
        {
            get => _name;
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ItemNotFoundException("Name is required");

                }
                _name = value;
            }
        }
        [Required]
        public string description
        {
            get => _description;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ItemNotFoundException("Description is required.");
                }
                _description = value;
            }
        }
        public string image { get; set; }
        [Required]
        [MaxLength(120)]
        public string slug { get; set; }
        public bool is_active { get; set; } = true;
            public void active()
        {
            is_active=true;
        }
        public void inactive()
        {
            is_active = false;
        }


    }
}
