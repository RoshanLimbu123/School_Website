using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface SpecialitiesRepository
    {
        void insert(Specialities specialities );
        void update(Specialities specialities);
        void delete(Specialities specialities);
        List<Specialities> getAll();
        Specialities getById(long page_id);
        Specialities getBySlug(string slug);
        List<Specialities> getByName(string title);
        IQueryable<Specialities> getQueryable();
    }
}
