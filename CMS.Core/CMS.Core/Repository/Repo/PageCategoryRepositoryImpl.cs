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
    public class PageCategoryRepositoryImpl : BaseRepositoryImpl<PageCategory>, PageCategoryRepository
    {
        private readonly AppDbContext _appDbContext;
        public PageCategoryRepositoryImpl(AppDbContext context, DetailsEncoder<PageCategory> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context;
        }

        public PageCategory getByName(string name)
        {
            return _appDbContext.page_categories.Where(a => a.name == name).SingleOrDefault();
        }
    }
}