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
    public class DesignationRepositoryImpl : BaseRepositoryImpl<Designation>, DesignationRepository
    {
        private AppDbContext _appDbContext;
        public DesignationRepositoryImpl(AppDbContext context, DetailsEncoder<Designation> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context;
        }
        public Designation getByPosition(string position)
        {
            return _appDbContext.designations.Where(a => a.position == position).SingleOrDefault();
        }
    }
}
