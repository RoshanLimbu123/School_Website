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
    public class VideoServiceImpl : VideoService
    {
        private readonly VideoMaker _videoMaker;
        private readonly VideoRepository _videoRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        public  VideoServiceImpl(VideoMaker videoMaker,VideoRepository videoRepository,IHostingEnvironment hostingEnvironment)
        {
            _videoMaker = videoMaker;
            _videoRepository = videoRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        public void enable(long video_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                { 
                Video video = _videoRepository.getById(video_id);
                    if(video == null)
                    {
                        throw new ItemNotFoundException($"Videos With Id { video_id } is not Found.");
                    }

                    video.enable();
                    _videoRepository.update(video);

                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void makeHomeVideo(long video_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var hasHomeVideo = _videoRepository.getHomeVideo();

                    if (hasHomeVideo != null)
                    {
                        hasHomeVideo.unmakeHomePage();
                        _videoRepository.update(hasHomeVideo);
                    }
                    var page = _videoRepository.getById(video_id);
                    if (page == null)
                        throw new ItemNotFoundException($"Video with id {video_id} doesnot exist.");
                    page.makeHomePage();
                    page.enable();
                    _videoRepository.update(page);
                    tx.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void delete(long video_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Video video = _videoRepository.getById(video_id);
                    if (video == null)
                    {
                        throw new ItemNotFoundException($"Video with ID {video_id} doesnot exist.");
                    }
                    _videoRepository.delete(video);
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void disable(long video_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var video = _videoRepository.getById(video_id);
                    if (video == null)
                    {
                        throw new ItemNotFoundException($"Video With Id {video_id} Doesnot Exist.");
                    }
                    video.disable();
                    _videoRepository.update(video);

                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void save(VideoDto videodto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Video videos = new Video();
                    _videoMaker.copy(videos, videodto);
                    _videoRepository.insert(videos);

                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void update(VideoDto videodto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Video video1 = _videoRepository.getById(videodto.video_id);
                    if (video1 == null)
                    {
                        throw new ItemNotFoundException($"Video with ID {videodto.video_id} doesnot Exit.");
                    }

                    _videoMaker.copy(video1, videodto);
                    _videoRepository.update(video1);
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

