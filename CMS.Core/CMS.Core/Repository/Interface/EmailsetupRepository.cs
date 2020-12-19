using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface EmailsetupRepository
    {
        void insert(EmailSetup email_Setup);
        void update(EmailSetup email_Setup);
        void delete(EmailSetup email_Setup);
        EmailSetup getById(long email_setup_id);

        List<EmailSetup> getAll();
        IQueryable<EmailSetup> getQueryable();
    }
}
