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
    public class CareerRepositoryImpl : BaseRepositoryImpl<Career>, CareerRepository
    {
        private readonly AppDbContext _appDbContext;
        public CareerRepositoryImpl(AppDbContext context, DetailsEncoder<Career> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) :base(context,detailsEncoder,htmlEncodingClassHelper)
        {
            _appDbContext = context;
        }
        public Career getBySlug(string slug)
        {
            return _appDbContext.careers.Where(a => a.slug.Contains(slug)).SingleOrDefault();
        }
    }
}
