using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Data.Repositories
{
    public class PostRepository(BlogDbContext _context) : BaseRepository<Post>(_context), IPostRepository
    {
        public async Task<Post?> GetByPostIdAndUserIdAsync(int postId, int userId, CancellationToken cancellationToken)
        {
            return await _dbSet
                .FirstOrDefaultAsync(p => p.UserId == userId && p.Id == postId, cancellationToken);
        }

        public async Task<bool> IsTitleDuplicatedAsync(
           int userId, string title, int? exceptSuiteId = null, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AnyAsync(p =>
                    p.UserId == userId
                    && p.Title.ToLower() == title.ToLower()
                    && (exceptSuiteId == null || p.Id != exceptSuiteId),
                cancellationToken);
        }
    }
}