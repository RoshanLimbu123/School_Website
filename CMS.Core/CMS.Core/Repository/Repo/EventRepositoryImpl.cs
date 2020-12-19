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
    public class EventRepositoryImpl : BaseRepositoryImpl<Event>, EventRepository
    {
        private readonly AppDbContext _appDbContext;
        public EventRepositoryImpl(AppDbContext context, DetailsEncoder<Event> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context,detailsEncoder,htmlEncodingClassHelper)
        {
            _appDbContext = context;
        }

        public Event getBySlug(string slug)
        {
            return _appDbContext.events.Where(a => a.slug.Contains(slug)).SingleOrDefault();
        }
    }
}
