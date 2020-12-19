using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Admin.FilterModel;
using CMS.Web.Areas.Core.Models;
using CMS.Web.Areas.Core.ViewModels;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/events")]
    public class EventController : Controller
    {
        private readonly EventRepository _eventRepository;
        private readonly EventService _eventService;
        private readonly IMapper _mapper;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly FileHelper _fileHelper;

        public EventController(EventRepository eventRepository, EventService eventService, IMapper mapper, PaginatedMetaService paginatedMetaService, FileHelper fileHelper)
        {
            _eventRepository = eventRepository;
            _eventService = eventService;
            _mapper = mapper;
            _paginatedMetaService = paginatedMetaService;
            _fileHelper = fileHelper;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index(EventFilter filter = null)
        {
            try
            {
                var events = _eventRepository.getQueryable();

                if (!string.IsNullOrWhiteSpace(filter.title))
                {
                    events = events.Where(a => a.image_name.Contains(filter.title));
                }

           
                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(events.Count(), filter.page, filter.number_of_rows);


                events = events.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);

                var eventDetails = events.ToList();

                var eventlIndexVM = getViewModelFrom(eventDetails);
                return View(eventlIndexVM);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return Redirect("/admin");
            }
        }

        [Route("new")]
        public IActionResult add()
        {
            EventModel model = new EventModel();
            return View(model);
        }

        [HttpPost]
        [Route("new")]
        public IActionResult add(EventModel model, IFormFile file = null, IFormFile form = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EventDto dto = new EventDto();
                    dto.event_id = model.event_id;
                    dto.title = model.title;
                    if (file != null)
                    {
                        dto.image_name = _fileHelper.saveImageAndGetFileName(file, model.title);
                        dto.file_name = _fileHelper.saveFileAndGetFileName(form, model.title);
                    }
                    dto.event_from_date = model.event_from_date;
                    dto.event_to_date = model.event_to_date;
                    dto.description = model.description;
                    dto.is_closed = dto.is_closed;
                    dto.time = model.time;
                    dto.venue = model.venue;

                    _eventService.save(dto);
                    AlertHelper.setMessage(this, "Event saved successfully.", messageType.success);
                    return RedirectToAction("index");


                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("new");
            }
            return View(model);
        }

        [HttpGet]
        [Route("edit/{event_id}")]
        public IActionResult edit(long event_id)
        {
            try
            {
                Event events = _eventRepository.getById(event_id);
                EventModel eventModel = _mapper.Map<EventModel>(events);

                return View(eventModel);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [Route("edit")]
        public IActionResult edit(EventModel model, IFormFile file = null, IFormFile form = null)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    EventDto dto = new EventDto();
                    dto.title = model.title;
                    dto.event_id = model.event_id;
                    dto.event_from_date = model.event_from_date;
                    dto.event_to_date = model.event_to_date;
                    dto.description = model.description;
                    dto.is_closed = dto.is_closed;
                    dto.time = model.time;
                    dto.venue = model.venue;

                    //if (model.image_name != null)
                    //{
                    //    dto.image_name = _fileHelper.saveImageAndGetFileName(file, model.title);
                    //}
                    //if (file != null)
                    //{
                    //    if (model.file_name != null)
                    //    {
                    //        deleteImage(model.file_name);
                    //    }
                    //    dto.file_name = _fileHelper.saveFileAndGetFileName(form, model.title);

                    //}
                    //Images
                    if (file != null)
                    {
                        if (model.image_name != null)
                        {
                            deleteImage(model.image_name);
                        }
                        dto.image_name = _fileHelper.saveImageAndGetFileName(file, $"{model.image_name} {model.event_id}");
                    }
                    if (file == null && string.IsNullOrEmpty(dto.image_name))
                    {
                        model.image_name = null;
                    }

                    //File.
                    if (form != null)
                    {
                        if (model.file_name != null)
                        {
                            deleteImage(model.file_name);
                        }
                        dto.file_name = _fileHelper.saveFileAndGetFileName(form, $"{model.file_name} {model.event_id}");

                    }
                    if (form == null && string.IsNullOrEmpty(dto.file_name))
                    {
                        model.file_name = null;
                    }

                    _eventService.update(dto);
                    AlertHelper.setMessage(this, "Event updated successfully.");
                    return RedirectToAction("index");
                }

            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(model);
        }

        private void deleteImage(string image_path)
        {
            _fileHelper.deleteImage(image_path, _fileHelper.getPathToImageFolder());
        }

        [HttpGet]
        [Route("close/{event_id}")]
        public IActionResult close(long event_id)
        {
            try
            {
                _eventService.close(event_id);
                AlertHelper.setMessage(this, "Event closed successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("unclose/{event_id}")]
        public IActionResult disable(long event_id)
        {
            try
            {
                _eventService.unclose(event_id);
                AlertHelper.setMessage(this, "Event unclosed successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("delete/{event_id}")]
        public IActionResult delete(long event_id)
        {
            try
            {
                _eventService.delete(event_id);
                AlertHelper.setMessage(this, "Event deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }
        private EventIndexViewModel getViewModelFrom(List<CMS.Core.Entity.Event> events)
        {
            EventIndexViewModel vm = new EventIndexViewModel();
            vm.event_details = new List<EventDetailModel>();
            foreach (var even in events)
            {
                var Events = _mapper.Map<EventDetailModel>(even);
                vm.event_details.Add(Events);
            }

            return vm;
        }

    }
}