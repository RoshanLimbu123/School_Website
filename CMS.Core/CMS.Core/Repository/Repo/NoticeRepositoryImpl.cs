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
    public class NoticeRepositoryImpl : BaseRepositoryImpl<Notice>, NoticeRepository
    {
        private readonly AppDbContext _context;
        public NoticeRepositoryImpl(AppDbContext context, DetailsEncoder<Notice> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _context = context;
        }

        public Notice getBySlug(string slug)
        {
            return _context.notices.Where(a => a.slug == slug).SingleOrDefault();
        }

        public Notice recentNotice()
        {
            return _context.notices.LastOrDefault();
        }
        

      
    }
}