using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class BlogCommentMakerImpl : BlogCommentMaker
    {
        public void copy(BlogComment blogComment, BlogCommentDto dto)
        {
            blogComment.blog_comment_id = dto.blog_comment_id;
            blogComment.blog_id = dto.blog_id;
            blogComment.comments = dto.comments;
            blogComment.comment_by = dto.comment_by;
            blogComment.comment_date = dto.comment_date;
            blogComment.email = dto.email;
        }
    }
}
