using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface CoursesRepository
    {
        void insert(Courses product);
        void update(Courses product);
        void delete(Courses product);
        List<Courses> getAll();
        List<Courses> getByName(string name);
        Courses getById(long gallery_id);
        Courses getBySlug(string slug);
        IQueryable<Courses> getQueryable();
    }
}
