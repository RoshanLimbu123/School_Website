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
    public class TeacherRepositoryImpI : BaseRepositoryImpl<Teacher>, TeacherRepository
    {
        private readonly AppDbContext _appDbContext;
        public TeacherRepositoryImpI(AppDbContext appDbContext, DetailsEncoder<Teacher> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(appDbContext, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = appDbContext;

        }

        public List<Teacher> getByName(string name)
        {
            return _appDbContext.teachers.Where(a => a.name == name).ToList();
        }

        public Teacher getBySlug(string slug)
        {
            return _appDbContext.teachers.Where(a => a.slug == slug).SingleOrDefault();
        }
    }
}
