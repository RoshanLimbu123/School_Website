using CMS.Core.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Core.Entity
{
    public class Page
    {
        private string _title;

        [Key]
        public long page_id { get; set; }

        [Required]
        public long page_category_id { get; set; }

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
        public bool is_home_page { get; set; } = false;

        [ForeignKey("page_category_id")]
        public virtual PageCategory page_category { get; set; }

        public void makeHomePage()
        {
            is_home_page = true;
        }
        public void unmakeHomePage()
        {
            is_home_page = false;
        }

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
