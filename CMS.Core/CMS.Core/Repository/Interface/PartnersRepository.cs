using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface PartnersRepository
    {
        void insert(Partners partners);
        void update(Partners partners);
        void delete(Partners partners);
        List<Partners> getAll();
        Partners getById(long partners_id);
        IQueryable<Partners> getQueryable();

    }
}
