using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.User.Exceptions
{
    public class NonEmptyValueException : CustomException
    {
        public NonEmptyValueException(string message = "Value must be provided.") : base(message)
        {

        }
    }
}
