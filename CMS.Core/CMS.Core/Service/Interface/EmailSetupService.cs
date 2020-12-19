using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
   public interface EmailSetupService
    {
        void delete(long email_setup_id);
        void update(EmailSetupDto emailSetupDto);
        void save(EmailSetupDto emailSetupDto);
    }
}
