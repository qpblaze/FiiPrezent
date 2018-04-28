using System.Threading.Tasks;
using FiiPrezent.Core;
using FiiPrezent.Core.Interfaces;
using FiiPrezent.Web.Helpers;
using FiiPrezent.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiiPrezent.Web.Controllers
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

        [Authorize]
        [Route("join")]
        public IActionResult Join()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [Route("join")]
        public async Task<IActionResult> Join(JoinEventViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            
            var result = await _participantsService.RegisterParticipantAsync(model.Code, User.GetNameIdentifier());

            if (result.Type != ResultStatusType.Ok)
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