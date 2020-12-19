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
    public class SpecialitiesRepositoryImpl : BaseRepositoryImpl<Specialities>, SpecialitiesRepository
    {
        private readonly AppDbContext _appDbContext;

        public SpecialitiesRepositoryImpl(AppDbContext context, DetailsEncoder<Specialities> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context;
        }

        List<Specialities> SpecialitiesRepository.getByName(string title)
        {
            return _appDbContext.specialities.Where(a => a.title == title).ToList();
        }

        Specialities SpecialitiesRepository.getBySlug(string slug)
        {
            return _appDbContext.specialities.Where(a => a.slug == slug).SingleOrDefault();
        }
    }
}