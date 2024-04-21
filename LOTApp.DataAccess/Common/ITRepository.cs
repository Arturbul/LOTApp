namespace LOTApp.DataAccess.Common
{
    public interface ITRepository<T>
        where T : class, new()
    {
        public IQueryable<T> AllEntities { get; }
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<int> Delete(T entity);
    }
}
