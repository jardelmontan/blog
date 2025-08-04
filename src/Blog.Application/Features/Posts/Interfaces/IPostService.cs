using Blog.Application.Features.Posts.Dtos;
using Blog.Domain.Common;

namespace Blog.Application.Features.Posts.Interfaces
{
    public interface IPostService
    {
        Task<Result<PostDto>> GetByPostIdAsync(int postId, CancellationToken cancellationToken);
        Task<Result<IEnumerable<PostDto>>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<PostDto>> CreateAsync(int userId, CreatePostRequest request, CancellationToken cancellationToken);
        Task<Result> UpdateAsync(int postId, int userId, UpdatePostRequest request, CancellationToken cancellationToken);
        Task<Result> DeleteAsync(int postId, CancellationToken cancellationToken);
    }
}