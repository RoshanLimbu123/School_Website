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
    public class GalleryImageRepositoryImpl : BaseRepositoryImpl<GalleryImage>, GalleryImageRepository
    {
        private readonly AppDbContext _appDbContext;

        public GalleryImageRepositoryImpl(AppDbContext context, DetailsEncoder<GalleryImage> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context;
        }

        public GalleryImage getByName(string name)
        {
            return _appDbContext.gallery_image.Where(a => a.title == name).SingleOrDefault();
        }
    }
}