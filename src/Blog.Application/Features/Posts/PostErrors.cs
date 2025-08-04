using Blog.Domain.Enums;
using Blog.Domain.Errors;

namespace Blog.Application.Features.Posts
{
    public static class PostErrors
    {
        public static readonly ResultError PostDoesNotExist = 
            new("post_does_not_exist", "Post does not exist.", ErrorType.NotFound);

        public static readonly ResultError TitleAlreadyExists = 
            new("title_already_exists", "there is already a post with the same name.", ErrorType.BusinessRule);
    }
}
