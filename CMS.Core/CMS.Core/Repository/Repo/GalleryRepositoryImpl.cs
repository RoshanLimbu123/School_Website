using CMS.Core.Data;
using CMS.Core.Entity;
using CMS.Core.Helper;
using CMS.Core.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace CMS.Core.Repository.Repo
{
  public class GalleryRepositoryImpl:BaseRepositoryImpl<Gallery>, GalleryRepository
    {
        private readonly AppDbContext _appDbContext;
        public GalleryRepositoryImpl(AppDbContext context, DetailsEncoder<Gallery> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context;
        }

       
    }
}
