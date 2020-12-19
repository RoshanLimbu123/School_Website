using CMS.Core.Entity;
using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Core.Dto
{
    public class GalleryDto
    {
        [Key]
        public long gallery_id { get; set; }

        public bool is_active { get; set; } = true;
      
        private string _name;
 
        public string name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("name is required");
                }
                _name = value;
            }
        }
        public string description { get; set; }
    }

}
