using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface ReceivedEmailRepository
    {
        void insert(ReceivedEmail email);
        List<ReceivedEmail> getAll();
        ReceivedEmail getById(long received_email_id);
        List<ReceivedEmail> getBySender(string sender_name);
        IQueryable<ReceivedEmail> getQueryable();
    }
}
