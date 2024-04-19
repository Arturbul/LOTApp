using AutoMapper;
using Core.Models;
using Core.ViewModels;

namespace Business.Profiles
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
