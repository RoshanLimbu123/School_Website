using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface BlogCommentRepository
    {
        void insert(BlogComment blogComment);
        void update(BlogComment blogComment);
        void delete(BlogComment blogComment);
        List<BlogComment> getAll();
        BlogComment getById(long blog_id);
        IQueryable<BlogComment> getQueryable();
    }
}
