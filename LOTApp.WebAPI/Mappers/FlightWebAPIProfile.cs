using AutoMapper;
using LOTApp.Core.Models;
using LOTApp.Core.ViewModels;
using LOTApp.WebAPI.RequestModels;

namespace LOTApp.WebAPI.Mappers
{
    public class FlightWebAPIProfile : Profile
    {
        public FlightWebAPIProfile()
        {
            CreateMap<Flight, FlightViewModel>().ReverseMap();
            CreateMap<FlightViewModel, CreateFlightRequest>().ReverseMap();
            CreateMap<Flight, CreateFlightRequest>().ReverseMap();
            CreateMap<Flight, UpdateFlightRequest>().ReverseMap();
        }
    }
}
