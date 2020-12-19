using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
public interface DetailsRepository
    {
        void insert(Details details);
        void update(Details details);
        void delete(Details details);
        List<Details> getAll();
        Details getById(long details_id);
        IQueryable<Details> getQueryable();
    
    }
}
