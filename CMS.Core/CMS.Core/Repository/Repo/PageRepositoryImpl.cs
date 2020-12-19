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
    public class PageRepositoryImpl : BaseRepositoryImpl<Page>, PageRepository
    {
        private readonly AppDbContext _appDbContext;

        public PageRepositoryImpl(AppDbContext context, DetailsEncoder<Page> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context;
        }

        public List<Page> getByName(string title)
        {
            return _appDbContext.pages.Where(a => a.title == title).ToList();
        }

        public Page getBySlug(string slug)
        {
            return _appDbContext.pages.Where(a => a.slug==slug).SingleOrDefault();
        }

        public Page getHomePage()
        {
            return _appDbContext.pages.Where(a => a.is_home_page == true).SingleOrDefault();
        }
    }
}