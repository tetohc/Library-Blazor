using Library.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ContextDb _contextDb;
        private readonly DbSet<T> _dbSet;

        public Repository(ContextDb contextDb)
        {
            _contextDb = contextDb;
            _dbSet = _contextDb.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _contextDb.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _contextDb.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _contextDb.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsyncWithIncludesAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id) ?? throw new KeyNotFoundException($"El registro con ID: {id} no existe.");
            return entity;
        }
    }
}