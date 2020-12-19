using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface EventRepository
    {

        void insert(Event events);
        void update(Event events);
        void delete(Event events);
        List<Event> getAll();
        Event getById(long event_id);
        IQueryable<Event> getQueryable();
        Event getBySlug(string slug);
    }
}
