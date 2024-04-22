using AutoMapper;
using LOTApp.Core.DTOs;
using LOTApp.Core.Models;
using LOTApp.Core.ViewModels;

namespace LOTApp.Business.Mappers
{
    public class FlightProfile : Profile
    {
        public FlightProfile()
        {
            CreateMap<Flight, FlightViewModel>().ReverseMap();
            CreateMap<FlightViewModel, CreateFlightDTO>().ReverseMap();
            CreateMap<Flight, CreateFlightDTO>().ReverseMap();
        }
    }
}
