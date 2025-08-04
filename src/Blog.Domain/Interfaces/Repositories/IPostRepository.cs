using Blog.Domain.Common.Interfaces;
using Blog.Domain.Entities;

namespace Blog.Domain.Interfaces.Repositories
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<Post?> GetByPostIdAndUserIdAsync(int postId, int userId, CancellationToken cancellationToken);
        Task<bool> IsTitleDuplicatedAsync(int userId, string title, int? exceptSuiteId = null, CancellationToken cancellationToken = default);
    }
}