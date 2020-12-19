
using CMS.Core.Data;
using CMS.Core.Entity;
using CMS.Core.Helper;
using CMS.Core.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Repo
{
    public class BlogRepositoryImpl : BaseRepositoryImpl<Blog>, BlogRepository
    {
        private readonly AppDbContext _appDbContext;
        public BlogRepositoryImpl(AppDbContext context, DetailsEncoder<Blog> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context; 
        }
        public Blog getBySlug(string slug)
        {
            return _appDbContext.blog.Where(a => a.slug == slug).SingleOrDefault();
        }
    }
}
