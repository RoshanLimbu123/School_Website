using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface BlogCommentService
    {

        void save(BlogCommentDto blogcomment_dto);
        void update(BlogCommentDto blogcomment_dto);
        void delete(long blog_id);
    }
}
