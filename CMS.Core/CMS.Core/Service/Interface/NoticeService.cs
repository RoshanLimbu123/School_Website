using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface NoticeService
    {
        void save(NoticeDto notice_dto);
        void update(NoticeDto notice_dto);
        void delete(long notice_id);
        void close(long notice_id);
        void unclose(long notice_id);
    }
}
