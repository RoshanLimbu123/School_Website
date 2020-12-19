using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Exceptions
{
    public class ItemUsedException : CustomException
    {
        public ItemUsedException(string message = "Specified item has already been used.") : base(message)
        {

        }
    }
}
