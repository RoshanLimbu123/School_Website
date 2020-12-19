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
    public class CoursesRepositoryImpl : BaseRepositoryImpl<Courses>, CoursesRepository
    {
        private readonly AppDbContext _appDbContext;

        public CoursesRepositoryImpl(AppDbContext context, DetailsEncoder<Courses> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context;
        }

        public List<Courses> getByName(string name)
        {
          return  _appDbContext.courses.Where(a => a.name == name).ToList();
        }

        public Courses getBySlug(string slug)
        {
            return _appDbContext.courses.Where(a => a.slug == slug).SingleOrDefault();
        }
    }
}