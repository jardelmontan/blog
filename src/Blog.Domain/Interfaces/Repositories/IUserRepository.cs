using Blog.Domain.Common.Interfaces;
using Blog.Domain.Entities;

namespace Blog.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
