using CMS.Core.Data;
using CMS.Core.Entity;
using CMS.Core.Helper;
using CMS.Core.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CMS.Core.Repository.Repo
{
   public class ClassesRepositoryImpl:BaseRepositoryImpl<Classes>, ClassesRepository
    {
        private readonly AppDbContext _appDbContext;
        public ClassesRepositoryImpl(AppDbContext context, DetailsEncoder<Classes> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context;
        }
    }
}
