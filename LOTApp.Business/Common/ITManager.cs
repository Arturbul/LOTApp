using LOTApp.Core.ViewModels;

namespace LOTApp.Business.Common
{
    public interface ITManager<T, TViewModel, TRequestDTO, IdType>
        where T : class, new()
        where TViewModel : class, new()
    {
        FlightViewModel? GetSingle(IdType id);
        Task<TViewModel> Create(TRequestDTO entity);
        Task<TViewModel> Update(TViewModel entity);
        Task<int> Delete(IdType entity);
    }
}
