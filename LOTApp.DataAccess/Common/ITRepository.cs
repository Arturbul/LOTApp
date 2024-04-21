using System.Linq.Expressions;

namespace LOTApp.DataAccess.Common
{
    public interface ITRepository<T>
        where T : class, new()
    {
        public IQueryable<T> AllEntities { get; }
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>>? expression = null);
        Task<T?> GetSingle(Expression<Func<T, bool>> expression);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<int> Delete(T entity);
    }
}
