using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface EmailSenderService
    {
        void send(ReceivedEmailDto email_detail,string email_password,string email_host, string email_port);
    }
}
