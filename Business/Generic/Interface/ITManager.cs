namespace Business.Generic.Interface
{
    public interface ITManager<T, TViewModel, IdType>
        where T : class, new()
        where TViewModel : class, new()
    {
        Task<IEnumerable<TViewModel>> Get();
        Task<TViewModel?> GetSingle(IdType id);
        Task<TViewModel> Create(TViewModel entity);
        Task<TViewModel> Update(TViewModel entity);
        Task<int> Delete(IdType entity);
    }
}
