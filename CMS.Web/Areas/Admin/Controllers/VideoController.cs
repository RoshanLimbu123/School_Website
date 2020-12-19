using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Admin.FilterModel;
using CMS.Web.Areas.Admin.ViewModels;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/video")]
    public class VideoController : Controller
    {
        private readonly VideoRepository _videoRepository;
        private readonly VideoService _videoService;
        private readonly IMapper _mapper;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly FileHelper _fileHelper;
        public VideoController(VideoRepository videoRepository, VideoService videoService, IMapper mapper, PaginatedMetaService paginatedMetaService, FileHelper fileHelper)
        {
            _videoRepository = videoRepository;
            _videoService = videoService;
            _mapper = mapper;
            _paginatedMetaService = paginatedMetaService;
            _fileHelper = fileHelper;
        }
        [Route("")]

        public IActionResult Index(VideoFilter filter = null)
        {
            var videos = _videoRepository.getQueryable();

            if (!string.IsNullOrWhiteSpace(filter.title))
            {
                videos = videos.Where(a => a.title.Contains(filter.title));
            }

            ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(videos.Count(), filter.page, filter.number_of_rows);


            videos = videos.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);

            var videoDetails = videos.ToList();

            var videoIndexVM = getViewModelFrom(videoDetails);
            return View(videoIndexVM);
        }

        private VideoIndexViewModel getViewModelFrom(List<Video> videoDetails)
        {
            VideoIndexViewModel vm = new VideoIndexViewModel();
            vm.videodetails = new List<VideoDetails>();
            foreach (var detail in videoDetails)
            {
                var video = _mapper.Map<VideoDetails>(detail);
                vm.videodetails.Add(video);
            }

            return vm;
        }

        [HttpGet]
        [Route("new")]
        public IActionResult add()
        {
            return View();
        }

        [HttpPost]
        [Route("new")]
        public IActionResult add(VideoDto video)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _videoService.save(video);
                    AlertHelper.setMessage(this, "Video saved successfully.", messageType.success);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(video);
        }


        [HttpGet]
        [Route("edit/{video_id}")]
        public IActionResult edit(long video_id)
        {
            try
            {
                var video1 = _videoRepository.getById(video_id);
                VideoDto dto = _mapper.Map<VideoDto>(video1);

                return View(dto);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult edit(VideoDto videodto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _videoService.update(videodto);
                    AlertHelper.setMessage(this, "Video updated successfully.");
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(videodto);
        }

        [HttpGet]
        [Route("delete/{video_id}")]
        public IActionResult delete(long video_id)
        {
            try
            {
                _videoService.delete(video_id);
                AlertHelper.setMessage(this, "Video deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("make-home-video/{video_id}")]
        public IActionResult makeHomeVideo(long video_id)
        {
            try
            {
                _videoService.makeHomeVideo(video_id);
                AlertHelper.setMessage(this, "Home Video made successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("enable/{video_id}")]
        public IActionResult enable(long video_id)
        {
            try
            {
                _videoService.enable(video_id);
                AlertHelper.setMessage(this, "Video enabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("disable/{video_id}")]
        public IActionResult disable(long video_id)
        {
            try
            {
                _videoService.disable(video_id);
                AlertHelper.setMessage(this, "Video disabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}