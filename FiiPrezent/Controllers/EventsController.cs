using System;
using System.Threading.Tasks;
using FiiPrezent.Entities;
using FiiPrezent.Interfaces;
using FiiPrezent.Models;
using FiiPrezent.Services;
using Microsoft.AspNetCore.Mvc;

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

        [Route("browse-event")]
        public async Task<IActionResult> Index()
        {
            return View(await _eventsService.ListAllEventsAsync());
        }

        [Route("create-event")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("create-event")]
        public async Task<IActionResult> Create(CreateEventViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // TODO: Add auto mapper
            var @event = new Event
            {
                Name = model.Name,
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

            return RedirectToAction(nameof(Index));
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

            return RedirectToAction(nameof(Index));
        }
    }
}