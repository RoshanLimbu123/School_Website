using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Repository.Interface;
using CMS.Web.Areas.Admin.FilterModel;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers
{
    [Route("video")]
    public class VideoController : Controller
    {
        private readonly VideoRepository _videoRepository;
        private readonly PaginatedMetaService _paginatedMetaService;
        public VideoController(VideoRepository videoRepository,PaginatedMetaService paginatedMetaService)
        {
            _videoRepository = videoRepository;
            _paginatedMetaService = paginatedMetaService;
        }
        public IActionResult Index(VideoFilter filter = null)
        {
            var videos = _videoRepository.getQueryable().Where(a =>a.is_enabled==true);
            ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(videos.Count(), filter.page, 5);
            videos = videos.Skip(filter.number_of_rows * (filter.page - 1)).Take(5);
            ViewBag.events = videos.ToList();
            return View(videos.ToList());
        }
    }
}