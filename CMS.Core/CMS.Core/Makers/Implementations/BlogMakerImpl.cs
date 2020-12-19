using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using CMS.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    
    public class BlogMakerImpl : BlogMaker
    {
        private readonly SlugGenerator _slugGenerator;

        public BlogMakerImpl(SlugGenerator slugGenerator)
        {
            _slugGenerator = slugGenerator;
        }
        public void copy(ref Blog blog, BlogDto blogDto)
        {
            blog.artical_by = blogDto.artical_by;
            blog.blog_id = blogDto.blog_id;
            blog.description = blogDto.description;
            blog.image_name = blogDto.image_name;
            blog.is_enabled = blogDto.is_enabled;
            blog.posted_on = blogDto.posted_on;
            blog.title = blogDto.title;
            blog.slug = _slugGenerator.generate(blogDto.title);
           
        }
    }
}
