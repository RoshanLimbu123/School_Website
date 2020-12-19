using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entity
{
    public class SpecialitiesCategory
    {
        private string _name;

        [Key]
        public long specialities_category_id { get; set; }

        [Required]
        [MaxLength(60)]
        public string name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exceptions.NonNullValueException("SpecialitiesCategory name is required.");
                }
                _name = value;
            }
        }
        public string description { get; set; }

        [MaxLength(70)]
        public string image_name { get; set; }

        [Required]
        [MaxLength(70)]
        public string slug { get; set; }
        public bool is_enabled { get; set; } = true;

        public virtual List<Specialities> specialities { get; set; }


        public void enable()
        {
            this.is_enabled = true;
            specialities.ForEach(c => c.is_enabled = true);
        }

        public void disable()
        {
            this.is_enabled = false;
            specialities.ForEach(c => c.is_enabled = false);
        }

    }
}
