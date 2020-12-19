using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Exceptions
{
    public class DuplicateItemException : CustomException
    {
        public DuplicateItemException(string message = "Item already exists.") : base(message)
        {

        }
    }
}
