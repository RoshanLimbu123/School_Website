using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class EmailSetupMakerImpl : EmailSetupMaker
    {
        public void copy(EmailSetup email_Setup, EmailSetupDto emailSetupDto)
        {
            email_Setup.email_setup_id = emailSetupDto.email_setup_id;
            email_Setup.port = emailSetupDto.port;
            email_Setup.host = emailSetupDto.host;
            email_Setup.email = emailSetupDto.email;
            email_Setup.password = emailSetupDto.password;
        }
    }
}
