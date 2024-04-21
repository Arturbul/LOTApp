using LOTApp.DataAccess.Common;
using LOTApp.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LOTApp.DataAccess.Repositories
{
    public class EFTRepository<T> : ITRepository<T>
        where T : class, new()
    {
        private readonly LOTContext _context;
        public EFTRepository(LOTContext context)
        {
            _context = context;
        }

        public IQueryable<T> AllEntities => _context.Set<T>();

        //GET
        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>>? expression = null)
        {
            if (expression != null)
            {
                return await _context.Set<T>()
                    .Where(expression)
                    .ToListAsync();
            }
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetSingle(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(expression);
        }

        //POST
        public async Task<T> Create(T entity)
        {
            var added = _context.Entry(entity);
            added.State = EntityState.Added;

            await _context.SaveChangesAsync();
            return added.Entity;
        }

        public async Task<T> Update(T entity)
        {
            var added = _context.Entry(entity);
            added.State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return added.Entity;
        }
        public async Task<int> Delete(T entity)
        {
            var deleted = _context.Entry(entity);
            deleted.State = EntityState.Deleted;

            return await _context.SaveChangesAsync();
        }
    }
}
