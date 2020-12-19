using CMS.Core.Data;
using CMS.Core.Entity;
using CMS.Core.Helper;
using CMS.Core.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Repository.Repo
{
    class RoutineRepositoryImpl:BaseRepositoryImpl<Routine>, RoutineRepository
    {
        private readonly AppDbContext _appDbContext;
        public RoutineRepositoryImpl(AppDbContext context, DetailsEncoder<Routine> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context;
        }
    }
}
