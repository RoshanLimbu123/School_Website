using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Core.Models;
using CMS.Web.Areas.Core.FilterModel;
using CMS.Web.Areas.Core.ViewModels;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/blog")]
    public class BlogController : Controller
    {
        private readonly BlogRepository _blogRepository;
        private readonly BlogService _blogService;
        private readonly BlogCommentService _blogCommentService;
        private readonly IMapper _mapper;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly FileHelper _fileHelper;
        private readonly BlogCommentRepository _blogCommentRepo;

        public BlogController(BlogRepository blogRepository, BlogService blogService, IMapper mapper, PaginatedMetaService paginatedMetaService, FileHelper fileHelper, BlogCommentRepository blogCommentRepo, BlogCommentService blogCommentService)
        {
            _blogRepository = blogRepository;
            _blogService = blogService;
            _mapper = mapper;
            _paginatedMetaService = paginatedMetaService;
            _fileHelper = fileHelper;
            _blogCommentRepo = blogCommentRepo;
            _blogCommentService = blogCommentService;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index(BlogFilter filter = null)
        {
            try
            {
                var blogs = _blogRepository.getQueryable();
                if (!string.IsNullOrWhiteSpace(filter.title))
                {
                    blogs = blogs.Where(a => a.title.Contains(filter.title));
                }
                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(blogs.Count(), filter.page, filter.number_of_rows);
                blogs = blogs.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);

                var blogsDetails = blogs.ToList();

                var blogIndexVM = getViewModelFrom(blogsDetails);
                return View(blogIndexVM);
            }
            catch (Exception ex)
            {

                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return Redirect("/admin");
            }
          
        }
        
        [Route("new")]
        public IActionResult add()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [Route("new")]
        public IActionResult add(BlogDto blog_dto, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        blog_dto.image_name = _fileHelper.saveImageAndGetFileName(file, blog_dto.title);
                    }
                    _blogService.save(blog_dto);
                    AlertHelper.setMessage(this, "Event saved successfully.", messageType.success);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(blog_dto);
        }

        [HttpGet]
        [Route("edit/{blog_id}")]
        public IActionResult edit(long blog_id)
        {
            try
            {
                var blogs = _blogRepository.getById(blog_id);
                BlogDto blogDto = _mapper.Map<BlogDto>(blogs);

                return View(blogDto);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [Route("edit")]
        public IActionResult edit(BlogDto blog_dto, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   
                    if (file != null)
                    {
                        blog_dto.image_name = _fileHelper.saveImageAndGetFileName(file, blog_dto.title);
                    }
                    _blogService.update(blog_dto);
                    AlertHelper.setMessage(this, "Blog updated successfully.");
                    return RedirectToAction("index");

                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(blog_dto);
        }

        [HttpGet]
        [Route("close/{blog_id}")]
        public IActionResult close(long blog_id)
        {
            try
            {
                _blogService.close(blog_id);
                AlertHelper.setMessage(this, "Blog closed successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("unclose/{blog_id}")]
        public IActionResult disable(long blog_id)
        {
            try
            {
                _blogService.unclose(blog_id);
                AlertHelper.setMessage(this, "Blog unclosed successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("delete/{blog_id}")]
        public IActionResult delete(long blog_id)
        {
            try
            {
                _blogService.delete(blog_id);
                AlertHelper.setMessage(this, "Blog deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

      

        [HttpGet]
        [Route("blogComment/{blog_id}")]
        public IActionResult blogComment(long blog_id)
        {
            try
            {
                var blogsComment = _blogCommentRepo.getQueryable().Where(a => a.blog_id == blog_id).ToList();
                var blogCommentVM = getViewModelFromBlog(blogsComment);

                return View(blogCommentVM);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }
        [HttpGet]
        [Route("blogCommentDelete/{blog_comment_id}")]
        public IActionResult blogCommentDelete(long blog_comment_id)
        {
            try
            {
                _blogCommentService.delete(blog_comment_id);
                AlertHelper.setMessage(this, "Blog Comment deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("blogCommentEdit/{blog_comment_id}")]
        public IActionResult blogCommentEdit(long blog_comment_id)
        {
            try
            {
                var blogComment = _blogCommentRepo.getById(blog_comment_id);
                BlogCommentDto blogCommentDto = _mapper.Map<BlogCommentDto>(blogComment);

                return View(blogCommentDto);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [Route("blogCommentEdit")]
        public IActionResult blogCommentEdit(BlogCommentDto blog_comment_dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _blogCommentService.update(blog_comment_dto);
                    AlertHelper.setMessage(this, "Blog Comment updated successfully.");
                    return RedirectToAction("index");

                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(blog_comment_dto);
        }


        private BlogCommentIndexViewModel getViewModelFromBlog(List<CMS.Core.Entity.BlogComment> blogComment)
        {
            BlogCommentIndexViewModel vm = new BlogCommentIndexViewModel();
            vm.blog_details = new List<BlogCommentDetailModel>();
            foreach (var blog in blogComment)
            {
                var Blogs = _mapper.Map<BlogCommentDetailModel>(blog);
                vm.blog_details.Add(Blogs);
            }

            return vm;
        }
        private BlogIndexViewModel getViewModelFrom(List<CMS.Core.Entity.Blog> blogs)
        {
            BlogIndexViewModel vm = new BlogIndexViewModel();
            vm.blog_details = new List<BlogDetailModel>();
            foreach (var blog in blogs)
            {
                var Blogs = _mapper.Map<BlogDetailModel>(blog);
                vm.blog_details.Add(Blogs);
            }

            return vm;
        }
    }
}