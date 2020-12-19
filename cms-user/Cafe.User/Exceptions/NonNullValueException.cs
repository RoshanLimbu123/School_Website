using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.User.Exceptions
{
    public class NonNullValueException : CustomException
    {
        public NonNullValueException(string message = "Value cannot be null") : base(message)
        {

        }
    }
}
