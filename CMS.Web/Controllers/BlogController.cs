using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Admin.FilterModel;
using CMS.Web.Areas.Core.FilterModel;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using CMS.Web.Models;
using CMS.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly BlogRepository _blogRepo;
        private readonly BlogService _blogService;
        private readonly EventRepository _eventRepo;
        private readonly IMapper _mapper;
        private readonly SetupRepository _setupRepo;
        private readonly BlogCommentService _blogCommentService;
        private readonly BlogCommentRepository _blogCommentRepo;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly NoticeRepository _noticeRepo;

        public BlogController(BlogRepository blogRepo, BlogService blogService, BlogCommentService blogCommentService, BlogCommentRepository blogCommentRepo, IMapper mapper, PaginatedMetaService paginatedMetaService, EventRepository eventRepo, SetupRepository setupRepo, NoticeRepository noticeRepo)
        {
            _blogRepo = blogRepo;
            _eventRepo = eventRepo;
            _blogService = blogService;
            _setupRepo = setupRepo;
            _mapper = mapper;
            _blogCommentRepo = blogCommentRepo;
            _blogCommentService = blogCommentService;
            _paginatedMetaService = paginatedMetaService;
            _noticeRepo = noticeRepo;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index(BlogFilter filter = null)
        {
            var eventValues = _eventRepo.getQueryable().Where(e => e.is_closed == false).Take(3).ToList();
            ViewBag.events = eventValues;

            var setupValues = _setupRepo.getQueryable().ToList();
            ViewBag.setup = setupValues;

            var blogs = _blogRepo.getQueryable();
            ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(blogs.Count(), filter.page, 5);

            blogs = blogs.Skip(filter.number_of_rows * (filter.page - 1)).Take(5);
            ViewBag.blogs = blogs.ToList();

            return View(blogs.ToList());
        }


        [HttpGet]
        [Route("detail/{slug}")]
        public IActionResult detail(BlogsModel model)
        {
            var setupValues = _setupRepo.getQueryable().ToList();
            ViewBag.setup = setupValues;
            var notice = _noticeRepo.getQueryable().Where(n => n.notice_expiry_date >= DateTime.Now && n.is_closed == false).Take(5).ToList();
            ViewBag.notices = notice;
            try
            {
                var blogDetails = _blogRepo.getBySlug(model.slug);

                //Blog
                model.blog_id = blogDetails.blog_id;
                model.artical_by = blogDetails.artical_by;
                model.description = blogDetails.description;
                model.image_name = blogDetails.image_name;
                model.posted_on = blogDetails.posted_on; ;
                model.title = blogDetails.title;
                model.is_enabled = blogDetails.is_enabled;
                model.slug = blogDetails.slug;

                var blogcomments = _blogCommentRepo.getQueryable().Where(a => a.blog_id == model.blog_id).ToList();

                List<BlogComments> blogComments = new List<BlogComments>();
                foreach (var comment in blogcomments)
                {
                    BlogComments blogcomment = new BlogComments();
                    blogcomment.comments = comment.comments;
                    blogcomment.comment_by = comment.comment_by;
                    blogcomment.comment_date = comment.comment_date;
                    blogcomment.email = comment.email;
                    blogComments.Add(blogcomment);
                }
                model.blog_comments = blogComments;

            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }

            return View(model);
        }

        [HttpPost]
        [Route("send-blogcontact")]
        public IActionResult sendBlog(BlogCommentDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var slug = _blogRepo.getById(dto.blog_id).slug;
                    _blogCommentService.save(dto);
                    AlertHelper.setMessage(this, "Comments saved successfully.", messageType.success);

                    return RedirectToAction("detail", "blog", new { @id = slug });
                }
                catch (Exception ex)
                {
                    TempData["message"] = ex.Message;
                    return RedirectToAction("detail");
                }
            }
            return View(dto);




        }
    }
}