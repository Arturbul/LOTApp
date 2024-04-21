namespace LOTApp.Business.Common
{
    public interface ITManager<T, TViewModel, IdType>
        where T : class, new()
        where TViewModel : class, new()
    {
        public IQueryable<TViewModel> AllEntities { get; }
        Task<IEnumerable<TViewModel>> Get();
        Task<TViewModel?> GetSingle(IdType id);
        Task<TViewModel> Create(TViewModel entity);
        Task<TViewModel> Update(TViewModel entity);
        Task<int> Delete(IdType entity);
    }
}
