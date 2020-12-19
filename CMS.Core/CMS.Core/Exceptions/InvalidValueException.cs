using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Exceptions
{
    public class InvalidValueException : CustomException
    {
        public InvalidValueException(string message = "The value provided is invalid") : base(message)
        {

        }
    }
}
