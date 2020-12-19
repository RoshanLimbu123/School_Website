using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
  public  interface TeacherRepository
    {
        void insert(Teacher teacher);
        void update(Teacher teacher);
        void delete(Teacher teacher);
        List<Teacher> getAll();
        List<Teacher> getByName(string name);
        Teacher getById(long gallery_id);
        Teacher getBySlug(string slug);
        IQueryable<Teacher> getQueryable();
    }
}
