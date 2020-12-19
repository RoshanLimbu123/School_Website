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
    public class OutletRepositoryImpl : BaseRepositoryImpl<Outlet>, OutletRepository
    {
        private readonly AppDbContext _appDbContext;

        public OutletRepositoryImpl(AppDbContext context, DetailsEncoder<Outlet> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context;
        }

        public Outlet getByName(string name)
        {
            return _appDbContext.outlets.Where(a => a.title == name).SingleOrDefault();
        }
    }
}