using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class ReceivedEmailMakerImpl : ReceivedEmailMaker
    {
        public void copy(ref ReceivedEmail received_email, ReceivedEmailDto received_email_dto)
        {
            received_email.subject = received_email_dto.subject.Trim();
            received_email.sender_email = received_email_dto.sender_email.Trim();
            received_email.first_name = received_email_dto.first_name.Trim();
            received_email.last_name = received_email_dto.last_name.Trim();
            received_email.message = received_email_dto.message.Trim();
            received_email.phone = received_email_dto.phone.Trim();
        }
    }
}
