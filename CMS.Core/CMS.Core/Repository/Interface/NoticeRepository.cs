using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface NoticeRepository
    {
        void insert(Notice notice);
        void update(Notice notice);
        void delete(Notice notice);
        List<Notice> getAll();
        Notice getById(long gallery_id);
        IQueryable<Notice> getQueryable();
        Notice recentNotice();

        Notice getBySlug(string slug);


    }
}
