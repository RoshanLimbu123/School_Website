using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace CMS.Core.Service.Implementation
{
    public class BlogServiceImpl : BlogService
    {
        private readonly BlogMaker _blogMaker;
        private readonly BlogRepository _blogRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public BlogServiceImpl(BlogMaker blogMaker, BlogRepository blogRepository,IHostingEnvironment hostingEnvironment)
        {
            _blogMaker = blogMaker;
            _hostingEnvironment = hostingEnvironment;
            _blogRepository = blogRepository;
        }

        public void close(long blog_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var blogs = _blogRepository.getById(blog_id);
                    if (blogs == null)
                    {
                        throw new ItemNotFoundException($"Blog with Id {blog_id} doesnot exist.");
                    }

                    blogs.disable();
                    _blogRepository.update(blogs);
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void delete(long blog_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var BlogCategory = _blogRepository.getById(blog_id);
                    if (BlogCategory == null)
                    {
                        throw new ItemNotFoundException($"Blog Category With Id {BlogCategory} doesnot Exist.");
                    }

                    _blogRepository.delete(BlogCategory);
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void save(BlogDto blog_dto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Blog blog = new Blog();
                    _blogMaker.copy(ref blog, blog_dto);
                    _blogRepository.insert(blog);
                    tx.Complete();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void unclose(long blog_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var blog = _blogRepository.getById(blog_id);
                    if (blog == null)
                    {
                        throw new ItemNotFoundException($"Blog Category With Id {blog_id} Doesnot Exist.");
                    }
                    blog.enable();
                    _blogRepository.update(blog);

                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void update(BlogDto blog_dto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {


                    Blog blogs = _blogRepository.getById(blog_dto.blog_id);

                    if (blogs == null)
                    {
                        throw new ItemNotFoundException($"Blog with ID {blog_dto.blog_id} doesnot Exit.");
                    }

                    if(blog_dto.image_name == null)
                    {
                        blog_dto.image_name = blogs.image_name;
                    }
                    
                    _blogMaker.copy(ref blogs, blog_dto);
                    _blogRepository.update(blogs);
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
