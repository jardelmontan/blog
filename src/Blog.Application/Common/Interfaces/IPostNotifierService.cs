using Blog.Application.Features.Posts.Dtos;

namespace Blog.Application.Common.Interfaces
{
    public interface IPostNotifierService
    {
        Task PostCreated(NotifyPostCreatedDto post);
    }
}
