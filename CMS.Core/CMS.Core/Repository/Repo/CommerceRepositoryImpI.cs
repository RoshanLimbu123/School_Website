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
    public class CommerceRepositoryImpI : BaseRepositoryImpl<Commerce>, CommerceRepository
    {
        private readonly AppDbContext _appDbContext;

        public CommerceRepositoryImpI(AppDbContext appDbContext,DetailsEncoder<Commerce> detilsEncoder,HtmlEncodingClassHelper htmlEncodingClassHelper):base(appDbContext,detilsEncoder,htmlEncodingClassHelper)
        {
           _appDbContext = appDbContext;
        }
        public List<Commerce> getByName(string title)
        {
            return _appDbContext.commerces.Where(a => a.title == title).ToList();
        }

        public Commerce getBySlug(string slug)
        {
            return _appDbContext.commerces.Where(a => a.slug == slug).SingleOrDefault();
        }
    }
}
