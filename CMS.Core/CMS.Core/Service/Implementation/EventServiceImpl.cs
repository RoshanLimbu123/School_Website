using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace CMS.Core.Service.Implementation
{
    public class EventServiceImpl : EventService
    {
        private readonly EventRepository _eventRepository;
        private readonly EventMaker _eventMaker;
        private readonly IHostingEnvironment _hostingEnvironment;
        public EventServiceImpl(EventRepository eventRepository, EventMaker eventMaker,IHostingEnvironment hostingEnvironment)
        {
            _eventRepository = eventRepository;
            _eventMaker = eventMaker;
            _hostingEnvironment = hostingEnvironment;
        }

        public void close(long event_id)
        {
            try
            {
                using(TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var events = _eventRepository.getById(event_id);
                    if(events == null)
                    {
                        throw new ItemNotFoundException($"Event with Id {event_id} doesnot exist.");
                    }

                    events.close();
                    _eventRepository.update(events);
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void delete(long event_id)
        {
            try
            {
                using(TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var EventCategory = _eventRepository.getById(event_id);
                    if(EventCategory == null)
                    {
                        throw new ItemNotFoundException($"Event Category With Id {EventCategory} doesnot Exist.");
                    }

                    _eventRepository.delete(EventCategory);
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void save(EventDto event_dto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Event events = new Event();
                    _eventMaker.copy(events, event_dto);
                    _eventRepository.insert(events);
                    tx.Complete();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void unclose(long event_id)
        {
            try
            {
                using(TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var events = _eventRepository.getById(event_id);
                    if(events == null)
                    {
                        throw new ItemNotFoundException($"Event Category With Id {event_id} Doesnot Exist.");
                    }
                    events.unclose();
                    _eventRepository.update(events);

                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void update(EventDto event_dto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                 
                    Event events = _eventRepository.getById(event_dto.event_id);

                    if (events == null)
                    {
                        throw new ItemNotFoundException($"Event with ID {event_dto.event_id} doesnot Exit.");
                    }
                    if (event_dto.image_name == null)
                    {
                        event_dto.image_name = events.image_name;
                    }
                    
                    if (event_dto.file_name == null)
                    {
                        event_dto.file_name = events.file_name;
                    }
                    _eventMaker.copy(events, event_dto);
                
                    _eventRepository.update(events);
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
