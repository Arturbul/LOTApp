using AutoMapper;
using LOTApp.Core.Models;
using LOTApp.Core.ViewModels;

namespace LOTApp.Business.Mappers
{
    public class FlightServiceProfile : Profile
    {
        public FlightServiceProfile()
        {
            CreateMap<Flight, FlightViewModel>().ReverseMap();
        }
    }
}
