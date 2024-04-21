using AutoMapper;
using LOTApp.Core.Models;
using LOTApp.Core.ViewModels;

namespace LOTApp.Business.Mappers
{
    public class FlightProfile : Profile
    {
        public FlightProfile()
        {
            CreateMap<Flight, FlightViewModel>();
            CreateMap<FlightViewModel, Flight>();
        }
    }
}
