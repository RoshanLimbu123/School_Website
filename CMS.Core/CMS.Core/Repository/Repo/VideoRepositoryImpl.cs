using CMS.Core.Data;
using CMS.Core.Entity;
using CMS.Core.Helper;
using CMS.Core.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Repo
{
    public class VideoRepositoryImpl : BaseRepositoryImpl<Video>, VideoRepository
{
        private readonly AppDbContext _appDbContext;

        public VideoRepositoryImpl(AppDbContext appDbContext, DetailsEncoder<Video> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(appDbContext, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = appDbContext;
        }
       
        Video VideoRepository.getHomeVideo()
        {
            return _appDbContext.videos.Where(a => a.is_home_video == true).SingleOrDefault();
        }
    }
    }

