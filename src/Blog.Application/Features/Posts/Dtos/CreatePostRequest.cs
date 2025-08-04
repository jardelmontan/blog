namespace Blog.Application.Features.Posts.Dtos
{
    public class CreatePostRequest
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
    }
}