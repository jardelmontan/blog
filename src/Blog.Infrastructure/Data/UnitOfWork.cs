using Blog.Domain.Interfaces;
using Blog.Infrastructure.Data.Context;

namespace Blog.Infrastructure.Data
{
    public class UnitOfWork(BlogDbContext context) : IUnitOfWork
    {
        private readonly BlogDbContext _context = context;

        public async Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
