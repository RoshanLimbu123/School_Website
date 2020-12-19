using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using CMS.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class EventMakerImpl : EventMaker
    {
        private SlugGenerator _slugGenerator;

        public EventMakerImpl(SlugGenerator slugGenerator)
        {
            _slugGenerator = slugGenerator;
        }
        public void copy(Event events, EventDto event_dto)
        {
            events.event_id = event_dto.event_id;
            events.description = event_dto.description;
            events.event_from_date = event_dto.event_from_date;
            events.event_to_date = event_dto.event_to_date;
            events.file_name = event_dto.file_name;
            events.image_name = event_dto.image_name;
            events.time = event_dto.time;
            events.title = event_dto.title;
            events.venue = event_dto.venue;

            events.is_closed = event_dto.is_closed;
            events.slug = _slugGenerator.generate(event_dto.title);
        }
    }
}
