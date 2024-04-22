using LOTApp.DataAccess.Common;
using LOTApp.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task<T> Create(T entity)
        {
            var added = _context.Entry(entity);
            added.State = EntityState.Added;

            await _context.SaveChangesAsync();
            return added.Entity;
        }

        public async Task<T> Update(T entity)
        {
            var updated = _context.Entry(entity);
            updated.State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return updated.Entity;
        }
        public async Task<int> Delete(T entity)
        {
            var deleted = _context.Entry(entity);
            deleted.State = EntityState.Deleted;

            return await _context.SaveChangesAsync();
        }
    }
}
