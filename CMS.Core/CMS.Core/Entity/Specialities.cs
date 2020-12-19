using CMS.Core.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Core.Entity
{
    public class Specialities
    {
        private string _title;

        [Key]
        public long specialities_id { get; set; }

        [Required]
        public long specialities_category_id { get; set; }

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
        [MaxLength(60)]
        public string slug { get; set; }

        public string description { get; set; }

        [MaxLength(70)]
        public string image_name { get; set; }
        public bool is_enabled { get; set; } = true;

        [ForeignKey("specialities_category_id")]
        public virtual SpecialitiesCategory specialities_category { get; set; }

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
