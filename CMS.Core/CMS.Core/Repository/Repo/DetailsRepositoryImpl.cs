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
    public class DetailsRepositoryImpl : BaseRepositoryImpl<Details>, DetailsRepository
    {
       
        public DetailsRepositoryImpl(AppDbContext context, DetailsEncoder<Details> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {

        }

     
    }
}
