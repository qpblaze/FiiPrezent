using FiiPrezent.Entities;
using FiiPrezent.Interfaces;
using FiiPrezent.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FiiPrezent.Controllers
{
    public class EventsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("browse")]
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.Events.ListAllAsync());
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

            var @event = (await _unitOfWork.Events.GetAsync(x => x.SecretCode == model.SecretCode)).SingleOrDefault();
            if(@event != null)
            {
                ModelState.AddModelError("SecretCode", "This code is already in use.");
                return View(model);
            }

            // TODO: Add auto mapper
            @event = new Event
            {
                Name = model.Name,
                Description = model.Description,
                SecretCode = model.SecretCode,
                Location = model.Location,
                Date = model.Date
            };

            await _unitOfWork.Events.AddAsync(@event);
            await _unitOfWork.CompletedAsync();

            return RedirectToAction(nameof(Index));
        }

        [Route("event")]
        public async Task<IActionResult> Details(Guid id)
        {
            Event @event = await _unitOfWork.Events.GetByIdAsync(id);

            if (@event == null)
                return RedirectToAction(nameof(HomeController.Index), "Home");

            @event.Participants = await _unitOfWork.Participants.GetAsync(x => x.EventId == @event.Id);

            return View(new EventViewModel(@event));
        }

        [HttpPost]
        [Route("event")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var @event = await _unitOfWork.Events.GetByIdAsync(id);

            if(@event != null)
            {
                _unitOfWork.Events.Delete(@event);
                await _unitOfWork.CompletedAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }

    class Pisica
    {
        public string Nume { get; set; }
        public Pisica(string nume)
        {
            this.Nume = nume;
        }
    }

    class A
    {
        public A()
        {
            Console.WriteLine("se apeleaza asta din clasa b");
        }
    }

    class B
    {
        public B()
            :base()
        {

        }
    }

    class Shape
    {
        public virtual double Area()
        {
            return 0;
        }
    }

    class Dreptunghi : Shape
    {
        public int L;
        public int H;

        public override double Area()
        {
            return L * H;
        }
    }


    interface IPoster
    {
        void Post();
    }

    class FacebookPoster : IPoster
    {
        public void Post()
        {
            Console.WriteLine("am postat pe facebook");
        }
    }

    class TwitterPoster : IPoster
    {
        public void Post()
        {
            Console.WriteLine("am postat pe twitter");
        }
    }
    
    public class Custom<T> 
        where T: class
    {
        public void CeTip()
        {
            Console.WriteLine(typeof(T));
        }
    }
}

