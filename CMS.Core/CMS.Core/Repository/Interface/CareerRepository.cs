using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface CareerRepository
    {
        void insert(Career career);
        void update(Career career);
        void delete(Career career);
        List<Career> getAll();
        Career getById(long career_id);
        IQueryable<Career> getQueryable();
        Career getBySlug(string slug);

    }
}
