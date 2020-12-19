using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Models
{
    public class BlogsModel
    {

        private string _description, _artical_by, _title, _comment_by, _email, _comments;

        [Key]
        [Display(Name = "ID")]
        public long blog_id { get; set; }

        public long blog_comment_id { get; set; }

        [Display(Name = "Title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a Title.")]
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



        [Display(Name = "Date")]
        public DateTime posted_on { get; set; } = DateTime.Now;

        [Display(Name = "Artical By")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a Artical By.")]
        public string artical_by
        {
            get => _artical_by;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Artical By is required.");
                }
                _artical_by = value;
            }
        }

        public string slug { get; set; }

        [Display(Name = "Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a Description.")]
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

        [Display(Name = "Image")]
        public string image_name { get; set; }


        public bool is_enabled { get; set; } = true;

        public void enable()
        {
            is_enabled = true;
        }

        public void disable()
        {
            is_enabled = false;
        }
        public string comment_by { get; set; }
        public string email { get; set; }
        public string comments { get; set; }

        public DateTime comment_date { get; set; } = DateTime.Now;

        public List<BlogComments> blog_comments { get; set; }
    }

    public class BlogComments
    {
        public string comment_by { get; set; }
        public DateTime comment_date { get; set; } = DateTime.Now;
        public string email { get; set; }
        public string comments { get; set; }

    }

}


