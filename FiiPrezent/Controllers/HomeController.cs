using System.Threading.Tasks;
using FiiPrezent.Interfaces;
using FiiPrezent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FiiPrezent.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventService _eventService;

        public HomeController(IEventService eventService)
        {
            _eventService = eventService;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RsvpViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var @event = await _eventService.RegisterParticipantAsync(model.Code, model.Name);
            if (@event == null)
            {
                ModelState.AddModelError<RsvpViewModel>(x => x.Code, "Wrong verification code.");
                return View(model);
            }

            return RedirectToAction(nameof(EventsController.Details), "Events", new
            {
                @event.Id
            });
        }
    }
}