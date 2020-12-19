using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entity
{
   public class FranchiseModel
    {
        [Key]
        public long franchise_model_id { get; set; }
        private string _title, _description;

       
        [Required]
        [MaxLength(50)]
        public string title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Title is required.");
                }
                _title = value;
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
                    throw new NonEmptyValueException("Description is required.");
                }
                _description = value;
            }
        }
        [Required]
        [MaxLength(60)]
        public string slug { get; set; }

        public string file_name { get; set; }

        public bool is_enabled { get; set; } = true;

        public void enable()
        {
            is_enabled = true;
        }

        public void disable()
        {
            is_enabled = false;
        }
    }

}
