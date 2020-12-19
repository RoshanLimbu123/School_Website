using CMS.Core.Data;
using CMS.Core.Entity;
using CMS.Core.Helper;
using CMS.Core.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Repository.Repo
{
   public class EnquiryRepositoryImpl : BaseRepositoryImpl<Enquiry>, EnquiryRepository
    {
        private readonly AppDbContext _appDbContext;

        public EnquiryRepositoryImpl(AppDbContext context, DetailsEncoder<Enquiry> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context;


        }
    
    }
}
