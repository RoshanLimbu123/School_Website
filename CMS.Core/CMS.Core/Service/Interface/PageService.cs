using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface PageService
    {
        void delete(long page_id);
        void save(PageDto pageDto);
        void update(PageDto pageDto);
        void enable(long page_id);
        void disable(long page_id);
        void makeHomePage(long page_id);
    }
}
