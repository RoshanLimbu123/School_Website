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
    public class FiscalYearRepositoryImpl : BaseRepositoryImpl<FiscalYear>, FiscalYearRepository
    {
        private AppDbContext _appDbContext;
        public FiscalYearRepositoryImpl(AppDbContext context, DetailsEncoder<FiscalYear> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context;
        }

        public FiscalYear getByCurrent()
        {
            return _appDbContext.fiscalYears.Where(a => a.is_current == true).SingleOrDefault();
        }
    }
}
