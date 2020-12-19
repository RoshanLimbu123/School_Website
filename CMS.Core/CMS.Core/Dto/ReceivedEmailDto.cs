using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace CMS.Core.Dto
{
    public class ReceivedEmailDto
    {
        private string _senderEmail, _message, _subject, _firstName, _lastName,_phone;
        
        [MaxLength(50)] 
        [DataType(DataType.EmailAddress)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter email address")]
        public string sender_email
        {
            get => _senderEmail;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Sender Email is required.");
                }
                _senderEmail = value;
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter first name")]
        public string first_name
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("First name is required.");
                }
                _firstName = value;
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter last name")]
        public string last_name
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Last name is required.");
                }
                _lastName = value;
            }
        }

       
        public string receiver_email { get; set; }
        
        [MaxLength(150)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a subject")]
        public string subject
        {
            get => _subject;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Subject is required.");
                }
                _subject = value;
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your message")]
        public string message
        {
            get => _message;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Message is required.");
                }
                _message = value;
            }
        }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your Phone")]
        public string phone
        {
            get => _phone;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Phone no is required.");
                }
                _phone = value;
            }
        }


        public bool isReceiverEmailValid()
        {
            Regex reg = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}$", RegexOptions.IgnoreCase);
            if (reg.IsMatch(receiver_email))
            {
                return true;
            }
            return false;
        }
    }
}
