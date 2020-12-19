using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entity
{
    public class EmailSetup
    {
        [Key]
        public long email_setup_id { get; set; }
        private string _password;
        [Required]
       
        public string password
        {
            get => _password;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Password must be provided.");
                }
                _password = value;
            }
        }
        [Required]
        [MaxLength(50)]
        private string _email;
        public string email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException(" Email is required.");
                }
                _email = value;
            }
        }
        [Required]
        [MaxLength(50)]
        private string _port;
        public string port  
        {
            get => _port;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException(" port is required.");
                }
                _port = value;
            }
        }
        [Required]
        [MaxLength(50)]
        private string _host;
        public string host
        {
            get => _host;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException(" host is required.");
                }
                _host = value;
            }
        }


    }
}
