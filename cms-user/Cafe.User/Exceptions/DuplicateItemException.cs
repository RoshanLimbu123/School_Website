using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.User.Exceptions
{
    public class DuplicateItemException : CustomException
    {
        public DuplicateItemException(string message = "Item already exists.") : base(message)
        {

        }
    }
}
