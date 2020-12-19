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
    public class ReceivedEmailRepositoryImpl : BaseRepositoryImpl<ReceivedEmail>, ReceivedEmailRepository
    {
        private readonly AppDbContext _appDbContext;

        public ReceivedEmailRepositoryImpl(AppDbContext context, DetailsEncoder<ReceivedEmail> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context;
        }

        public List<ReceivedEmail> getBySender(string sender_name)
        {
            return _appDbContext.received_emails.Where(a => a.sender_email == sender_name).ToList();
        }
    }
}
