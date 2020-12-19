using CMS.Core.Entity;
using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Core.Dto
{
    public class RoutineDto
    {
        private string _title;
        [Key]
        public long routine_id { get; set; }
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

        [ForeignKey("class_id")]
        public virtual Classes classes { get; set; }

        [Display(Name = "Classes")]
        public long class_id { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        public DateTime start_date { get; set; } = DateTime.Now;
        [Display(Name = "End Date")]
        public DateTime end_date { get; set; }

        [Display(Name = "Image")]
        public string image { get; set; }
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
