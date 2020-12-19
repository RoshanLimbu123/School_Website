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
    public class ItemCategoryRepositoryImpl : BaseRepositoryImpl<ItemCategory>, ItemCategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public ItemCategoryRepositoryImpl(AppDbContext context, DetailsEncoder<ItemCategory> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context;
        }

        public ItemCategory getByName(string name)
        {
            return _appDbContext.itemCategories.Where(a => a.name == name).SingleOrDefault();
        }

        public ItemCategory getBySlug(string slug)
        {
            return _appDbContext.itemCategories.Where(a => a.slug == slug).SingleOrDefault();
        }
    }
}