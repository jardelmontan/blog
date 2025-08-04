namespace Blog.Application.Features.Posts.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int UserId { get; set; }
    }
}