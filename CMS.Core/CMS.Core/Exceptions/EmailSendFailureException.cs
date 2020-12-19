using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Exceptions
{
    public class EmailSendFailureException:Exception
    {
        public EmailSendFailureException(string message):base(message)
        {

        }
    }
}
