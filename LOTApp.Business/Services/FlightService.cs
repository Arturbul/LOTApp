using AutoMapper;
using LOTApp.Core.Models;
using LOTApp.Core.ViewModels;
using LOTApp.DataAccess.Repositories;

namespace LOTApp.Business.Services
{
    public class FlightService : IFlightService
    {
        private readonly IMapper _mapper;
        private readonly IFlightRepository _repository;
        public FlightService(IMapper mapper, IFlightRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public IQueryable<FlightViewModel> AllEntities => _mapper.Map<IQueryable<FlightViewModel>>(_repository.AllEntities);

        public async Task<IEnumerable<FlightViewModel>> Get()
        {
            var flights = await _repository.Get();
            return _mapper.Map<IEnumerable<FlightViewModel>>(flights);
        }

        public async Task<IEnumerable<FlightViewModel>> Get(
            int? id, string? flightNumber,
            DateTime? departTimeFrom, DateTime? departTimeTo,
            string? departLocation,
            string? arrivalLocation,
            int? planeType)
        {
            var query = _repository.AllEntities;

            if (id != null)
            {
                query = query.Where(x => x.Id.Equals(id));
            }

            if (departTimeFrom != null)
            {
                query = query.Where(x => x.DepartTime.CompareTo(departTimeFrom) >= 0);
            }

            if (departTimeTo != null)
            {
                query = query.Where(x => x.DepartTime.CompareTo(departTimeTo) <= 0);
            }

            if (!string.IsNullOrEmpty(flightNumber))
            {
                query = query.Where(x => x.FlightNumber.Equals(flightNumber));
            }

            if (!string.IsNullOrEmpty(departLocation))
            {
                query = query.Where(x => x.DepartLocation.Equals(departLocation));
            }

            if (!string.IsNullOrEmpty(arrivalLocation))
            {
                query = query.Where(x => x.ArrivalLocation.Equals(arrivalLocation));
            }

            if (planeType != null)
            {
                query = query.Where(x => x.PlaneType.Equals(planeType));
            }

            return _mapper.Map<IEnumerable<FlightViewModel>>(query.ToList());
        }

        public async Task<FlightViewModel?> GetSingle(int id)
        {
            var flight = await _repository.GetSingle(x => x.Id == id);
            return _mapper.Map<FlightViewModel>(flight);
        }
        public async Task<FlightViewModel> Create(FlightViewModel entityVM)
        {
            var entity = _mapper.Map<Flight>(entityVM);
            var result = await _repository.Create(entity);

            return _mapper.Map<FlightViewModel>(result);
        }
        public async Task<FlightViewModel> Update(FlightViewModel entityVM)
        {
            var entity = _mapper.Map<Flight>(entityVM);
            var result = await _repository.Update(entity);

            return _mapper.Map<FlightViewModel>(result);
        }
        public async Task<int> Delete(int id)
        {
            var entity = await _repository.GetSingle(x => x.Id == id);
            if (entity == null)
            {
                return 0;
            }
            return await _repository.Delete(entity);
        }
    }
}
