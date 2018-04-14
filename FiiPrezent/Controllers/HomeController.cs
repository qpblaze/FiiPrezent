using System;
using System.Threading.Tasks;
using FiiPrezent.Entities;
using FiiPrezent.Helpers;
using FiiPrezent.Interfaces;
using FiiPrezent.Models;
using FiiPrezent.Services;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [Route("join")]
        public IActionResult Join()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [Route("join")]
        public async Task<IActionResult> Join(RsvpViewModel model)
        {
            if (!ModelState.IsValid) 
                return View(model);

            Participant participant = new Participant
            {
                NameIdentifier = User.GetNameIdentifier(),
                Name = User.Identity.Name,
                ImagePath = User.GetProfileImage(),
                Email = User.GetEmail()
            };

            var result = await _participantsService.RegisterParticipantAsync(model.Code, participant);

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