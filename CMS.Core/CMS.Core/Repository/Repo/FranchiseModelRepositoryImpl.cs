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
    public class FranchiseModelRepositoryImpl : BaseRepositoryImpl<FranchiseModel>, FranchiseModelRepository
    {
        private readonly AppDbContext _appDbContext;

        public FranchiseModelRepositoryImpl(AppDbContext context, DetailsEncoder<FranchiseModel> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context;
        }

        public FranchiseModel getByName(string name)
        {
            return _appDbContext.franchiseModels.Where(a => a.title == name).SingleOrDefault();
        }

        public FranchiseModel getBySlug(string slug)
        {
            return _appDbContext.franchiseModels.Where(a => a.slug == slug).SingleOrDefault();
        }

      

    }

}