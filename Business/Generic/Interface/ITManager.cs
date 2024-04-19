namespace Business.Generic.Interface
{
    public interface ITManager<T, TViewModel>
        where T : class, new()
        where TViewModel : class, new()
    {
        Task<IEnumerable<TViewModel>> Get();
        Task<TViewModel?> GetSingle(int id);
        Task<TViewModel> Create(TViewModel entity);
        Task<TViewModel> Update(TViewModel entity);
        Task<int> Delete(int entity);
    }
}
