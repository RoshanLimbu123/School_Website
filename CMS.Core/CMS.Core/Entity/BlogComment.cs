using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Core.Entity
{
    public class BlogComment
    {
        private string _comment_by;
        private string _email;
        private string _comment;

        [Key]
        public long blog_comment_id { get; set; }

        [ForeignKey("blog_id")]
        public virtual Blog blog { get; set; }

        public long blog_id { get; set; }

        [Required]
        [MaxLength(200)]
        public string comment_by { get => _comment_by;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Name is Required.");
                }
                _comment_by = value;
            }
      }

        [Required]
        public DateTime comment_date { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(300)]
        public string email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Email is Required.");
                }
                _email = value;
            }
        }


        [Required]
        [MaxLength(5000)]
        public string comments { get => _comment;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Comment is Required.");
                }
                _comment = value;
            }
        }


    }
}
