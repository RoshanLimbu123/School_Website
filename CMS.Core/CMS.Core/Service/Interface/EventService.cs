using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface EventService
    {
        void save(EventDto event_dto);
        void update(EventDto event_dto);
        void delete(long event_id);
        void close(long event_id);
        void unclose(long event_id);
    }
}
