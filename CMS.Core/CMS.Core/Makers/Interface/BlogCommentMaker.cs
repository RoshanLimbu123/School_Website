using CMS.Core.Dto;
using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Interface
{
    public interface BlogCommentMaker
    {
        void copy(BlogComment blogComment, BlogCommentDto dto);
    }
}
