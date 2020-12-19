using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface BlogRepository
    {
        void insert(Blog blog);
        void update(Blog blog);
        void delete(Blog blog);
        List<Blog> getAll();
        Blog getById(long blog_id);
        IQueryable<Blog> getQueryable();
        Blog getBySlug(string slug);
    }
}
