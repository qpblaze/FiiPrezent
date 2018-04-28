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

            CreateMap<Event, BrowseEventViewModel>();

            CreateMap<Event, EventViewModel>()
                .ForMember(x => x.NameIdentifier, o => o.MapFrom(s => s.Account.NameIdentifier));

            CreateMap<Participant, ParticipantViewModel>()
                .ForMember(x => x.Name, o => o.MapFrom(s => s.Account.Name))
                .ForMember(x => x.Picture, o => o.MapFrom(s => s.Account.Picture));
        }
    }
}