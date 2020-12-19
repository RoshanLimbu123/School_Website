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
    public class BlogCommentServiceImpl : BlogCommentService
    {
        private readonly BlogCommentMaker _blogCommentMaker;
        private readonly BlogCommentRepository _blogCommentRepo;
        private readonly IHostingEnvironment _hostingEnvironment;
        public BlogCommentServiceImpl(BlogCommentMaker blogCommentMaker, BlogCommentRepository blogCommentRepo, IHostingEnvironment hostingEnvironment)
        {
            _blogCommentMaker = blogCommentMaker;
            _blogCommentRepo = blogCommentRepo;
            _hostingEnvironment = hostingEnvironment;
        }
        public void delete(long blog_id)
        {
            try
            {
                using(TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var BlogCommentCategory = _blogCommentRepo.getById(blog_id);
                    if (BlogCommentCategory == null)
                    {
                        throw new ItemNotFoundException($"Blog Comment Category With Id {BlogCommentCategory} doesnot Exist.");
                    }

                    _blogCommentRepo.delete(BlogCommentCategory);
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void save(BlogCommentDto blogcomment_dto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    BlogComment blogComment = new BlogComment();
                    _blogCommentMaker.copy(blogComment, blogcomment_dto);
                    _blogCommentRepo.insert(blogComment);
                    tx.Complete();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void update(BlogCommentDto blogcomment_dto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {


                    BlogComment blogcomment = _blogCommentRepo.getById(blogcomment_dto.blog_comment_id);

                    if (blogcomment == null)
                    {
                        throw new ItemNotFoundException($"Blog Comment with ID {blogcomment_dto.blog_comment_id} doesnot Exit.");
                    }
                    _blogCommentMaker.copy(blogcomment, blogcomment_dto);
                    _blogCommentRepo.update(blogcomment);
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

