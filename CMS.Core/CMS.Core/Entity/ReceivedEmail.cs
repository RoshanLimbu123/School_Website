using CMS.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Core.Entity
{
    public class ReceivedEmail
    {
        private string _senderEmail, _message,_subject,_firstName,_lastName,_phone;

        [Key]
        public long received_email_id { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
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

        [Required]
        [MaxLength(30)]
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

        [Required]
        [MaxLength(30)]
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

        [Required]
        [MaxLength(150)]
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

        [Required]
        [MaxLength(1000)]
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

        [Required]
        [MaxLength(1000)]
        public string phone
        {
            get => _phone;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NonEmptyValueException("Phone is required.");
                }
                _phone = value;
            }
        }

        public DateTime date { get; set; } = DateTime.Now;

    }
}
