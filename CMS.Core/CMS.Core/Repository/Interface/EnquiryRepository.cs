using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
   public interface EnquiryRepository
    {
        void insert(Enquiry enquiry);
        void update(Enquiry enquiry);
        void delete(Enquiry enquiry);
        List<Enquiry> getAll();
        Enquiry getById(long enquiry_id);
        IQueryable<Enquiry> getQueryable();
    }
}
