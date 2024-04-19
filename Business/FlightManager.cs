using AutoMapper;
using Business.Interface;
using Core.Models;
using Core.ViewModels;
using DataAccess.Interface;

namespace Business
{
    public class FlightManager : IFlightManager
    {
        private readonly IMapper _mapper;
        private readonly IFlightRepository _repository;
        public FlightManager(IMapper mapper, IFlightRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<IEnumerable<FlightViewModel>> Get()
        {
            var flights = await _repository.Get();
            return _mapper.Map<IEnumerable<FlightViewModel>>(flights);
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
