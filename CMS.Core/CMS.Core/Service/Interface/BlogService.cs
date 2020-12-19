using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface BlogService
    {

        void save(BlogDto blog_dto);
        void update(BlogDto blog_dto);
        void delete(long blog_id);
        void close(long blog_id);
        void unclose(long blog_id);
    }
}
