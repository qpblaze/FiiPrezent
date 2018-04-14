using FiiPrezent.Entities;
using FiiPrezent.Interfaces;
using FiiPrezent.Models;
using FiiPrezent.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FiiPrezent.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventsService _eventsService;

        public EventsController(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }

        private void AddModelErrors(ResultStatus result)
        {
            foreach (var error in result.GetErrors())
                ModelState.AddModelError(error.Key, error.Value);
        }

        [Route("browse")]
        public async Task<IActionResult> Browse()
        {
            BroseEventsViewModel model = new BroseEventsViewModel
            {
                Events = await _eventsService.ListAllEventsAsync(),
                Filter = new FilterEventsViewModel()
            };

            return View(model);
        }

        [HttpPost]
        [Route("browse")]
        public async Task<IActionResult> Browse(BroseEventsViewModel model)
        {
            var events = (await _eventsService.ListAllEventsAsync()).AsQueryable();

            if (!string.IsNullOrEmpty(model.Filter.Name))
                events = events.Where(x => x.Name.ToLower().Contains(model.Filter.Name));

            if (!string.IsNullOrEmpty(model.Filter.Location))
                events = events.Where(x => x.Location.ToLower().Contains(model.Filter.Location));

            if (!string.IsNullOrEmpty(model.Filter.Date))
                events = events.Where(x => x.Date.ToString().Contains(model.Filter.Date));

            model.Events = events.AsEnumerable();

            return View(model);
        }

        [Authorize]
        [Route("create-event")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [Route("create-event")]
        public async Task<IActionResult> Create(CreateEventViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            
            var @event = new Event
            {
                AccountId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value,
                Name = model.Name,
                ImagePath = model.ImagePath ?? "/img/event-placeholder.jpg",
                Description = model.Description,
                SecretCode = model.SecretCode,
                Location = model.Location,
                Date = model.Date
            };

            var result = await _eventsService.CreateEventAsync(@event);

            if (!result.Succeded)
            {
                AddModelErrors(result);
                return View(model);
            }

            return RedirectToAction(nameof(Browse));
        }

        [Route("event")]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _eventsService.GetEvent(id);

            if (result.ErrorType == ErrorType.NotFound)
                return NotFound();

            return View(new EventViewModel(result.Object));
        }

        [HttpPost]
        [Route("event")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _eventsService.DeleteEvent(id);

            if (result.ErrorType == ErrorType.NotFound)
                return NotFound();

            return RedirectToAction(nameof(Browse));
        }
    }
}