using System.Linq.Expressions;

namespace DataAccess.Generic.Interface
{
    public interface ITRepository<T>
        where T : class, new()
    {
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>>? expression);
        Task<T?> GetSingle(Expression<Func<T, bool>> expression);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<int> Delete(T entity);
    }
}
