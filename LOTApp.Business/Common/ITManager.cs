using LOTApp.Core.ViewModels;

namespace LOTApp.Business.Common
{
    public interface ITManager<T, TViewModel, IdType>
        where T : class, new()
        where TViewModel : class, new()
    {
        FlightViewModel? GetSingle(IdType id);
        Task<TViewModel> Create(TViewModel entity);
        Task<TViewModel> Update(TViewModel entity);
        Task<int> Delete(IdType entity);
    }
}
