using CMS.Core.Dto;
using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Interface
{
    public interface ReceivedEmailMaker
    {
        void copy(ref ReceivedEmail received_email, ReceivedEmailDto received_email_dto);
    }
}
