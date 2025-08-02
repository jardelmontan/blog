using Blog.Domain.Common.Interfaces;
using Blog.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Blog.Infrastructure.Data.Repositories
{
    public class BaseRepository<T>(BlogDbContext context) : IBaseRepository<T> where T : class
    {
        protected readonly BlogDbContext _context = context;
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbSet.FindAsync([id], cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(entities);
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        public void Update(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _dbSet.Update(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            ArgumentNullException.ThrowIfNull(entities);
            _dbSet.RemoveRange(entities);
        }
    }
}
