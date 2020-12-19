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
    public class SetupRepositoryImpl : BaseRepositoryImpl<Setup>, SetupRepository
    {
        private readonly AppDbContext _appDbContext;

        public SetupRepositoryImpl(AppDbContext context, DetailsEncoder<Setup> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context;
        }

        public Setup getByKey(string key)
        {
            return _appDbContext.setup.Where(a => a.key == key).SingleOrDefault();
        }
    }
}