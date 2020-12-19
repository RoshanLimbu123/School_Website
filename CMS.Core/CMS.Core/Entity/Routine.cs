using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Core.Entity
{

  public class Routine
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
        [Required]
        public long class_id { get; set; }
        [Required]
        public DateTime start_date { get; set; } =  DateTime.Now;
        [Required]
        public DateTime end_date { get; set; }
        [Required]
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












