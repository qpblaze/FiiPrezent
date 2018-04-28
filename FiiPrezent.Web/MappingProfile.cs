using AutoMapper;
using FiiPrezent.Core.Entities;
using FiiPrezent.Web.Models;

namespace FiiPrezent.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, UpdateEventViewModel>();
            CreateMap<UpdateEventViewModel, Event>();
            
            CreateMap<CreateEventViewModel, Event>();
            CreateMap<Event, EventViewModel>();
        }
    }
}