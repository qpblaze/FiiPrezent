using System.Threading.Tasks;
using FiiPrezent.Interfaces;
using FiiPrezent.Models;
using FiiPrezent.Services;
using Microsoft.AspNetCore.Mvc;

namespace FiiPrezent.Controllers
{
    public class HomeController : Controller
    {
        private readonly IParticipantsService _participantsService;

        public HomeController(IParticipantsService participantsService)
        {
            _participantsService = participantsService;
        }

        private void AddModelErrors(ResultStatus result)
        {
            foreach (var error in result.GetErrors())
                ModelState.AddModelError(error.Key, error.Value);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RsvpViewModel model)
        {
            if (!ModelState.IsValid) 
                return View(model);

            var result = await _participantsService.RegisterParticipantAsync(model.Code, model.Name);

            if (!result.Succeded)
            {
                AddModelErrors(result);
                return View(model);
            }

            return RedirectToAction(nameof(EventsController.Details), "Events", new
            {
                Id = result.Object
            });
        }
    }
}