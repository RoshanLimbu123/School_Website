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
    public class SpecialitiesCategoryRepositoryImpl : BaseRepositoryImpl<SpecialitiesCategory>, SpecialitiesCategoryRepository
    {
        private readonly AppDbContext _appDbContext;
        public SpecialitiesCategoryRepositoryImpl(AppDbContext context, DetailsEncoder<SpecialitiesCategory> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context;
        }

        public SpecialitiesCategory getByName(string name)
        {
            return _appDbContext.specialitiesCategories.Where(a => a.name == name).SingleOrDefault();
        }
    }
}