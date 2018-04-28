using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FiiPrezent.Core;
using FiiPrezent.Core.Entities;
using FiiPrezent.Core.Interfaces;
using FiiPrezent.Web.Helpers;
using FiiPrezent.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiiPrezent.Web.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventsService _eventsService;
        private readonly IMapper _mapper;

        public EventsController(IEventsService eventsService, IMapper mapper)
        {
            _eventsService = eventsService;
            _mapper = mapper;
        }

        private void AddModelErrors(ResultStatus result)
        {
            foreach (var error in result.GetErrors())
                ModelState.AddModelError(error.Key, error.Value);
        }

        [Route("browse")]
        public async Task<IActionResult> Browse()
        {
            var events = await _eventsService.ListAllEventsAsync();

            var model = new BrowseViewModel
            {
                Events = events.Select(x => new BrowseEventViewModel(x)),
                Filter = new FilterEventsViewModel()
            };

            return View(model);
        }

        [HttpPost]
        [Route("browse")]
        public async Task<IActionResult> Browse(BrowseViewModel model)
        {
            var events = (await _eventsService.ListAllEventsAsync()).AsQueryable();

            if (!string.IsNullOrEmpty(model.Filter.Name))
                events = events.Where(x => x.Name.ToLower().Contains(model.Filter.Name));

            if (!string.IsNullOrEmpty(model.Filter.Location))
                events = events.Where(x => x.Location.ToLower().Contains(model.Filter.Location));

            if (!string.IsNullOrEmpty(model.Filter.Date))
                events = events.Where(x => x.Date.ToString().Contains(model.Filter.Date));

            model.Events = events.Select(x => new BrowseEventViewModel(x));

            return View(model);
        }

        [Authorize]
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [Route("create")]
        public async Task<IActionResult> Create(CreateEventViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var @event = _mapper.Map<CreateEventViewModel, Event>(model);
            @event.ImagePath = model.ImagePath ?? "/img/event-placeholder.jpg";

            var result = await _eventsService.CreateEventAsync(@event, User.GetNameIdentifier());

            if (result.Type != ResultStatusType.Ok)
            {
                AddModelErrors(result);
                return View(model);
            }

            return RedirectToAction(nameof(Browse));
        }

        [Authorize]
        [Route("update")]
        public async Task<IActionResult> Update(Guid id)
        {
            Event @event = await _eventsService.GetEventAsync(id, false);

            return View(_mapper.Map<Event, UpdateEventViewModel>(@event));
        }

        [HttpPost]
        [Authorize]
        [Route("update")]
        public async Task<IActionResult> Update(UpdateEventViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var @event = _mapper.Map<UpdateEventViewModel, Event>(model);
            @event.ImagePath = model.ImagePath ?? "/img/event-placeholder.jpg";

            var result = await _eventsService.UpdateEventAsync(@event);

            if (result.Type != ResultStatusType.Ok)
            {
                AddModelErrors(result);
                return View(model);
            }

            return RedirectToAction(nameof(Browse));
        }

        [Route("event")]
        public async Task<IActionResult> Details(Guid id)
        {
            var @event = await _eventsService.GetEventAsync(id);

            if (@event == null)
                return NotFound();

            return View(new EventViewModel(@event));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _eventsService.DeleteEvent(id);

            if (result.Type == ResultStatusType.NotFound)
                return NotFound();

            return RedirectToAction(nameof(Browse));
        }

    }
}