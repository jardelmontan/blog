namespace Blog.Application.Features.Posts.Dtos
{
    public  class NotifyPostCreatedDto
    {
        public int PostId { get; set; }
        public string Author { get; set; } = null!;
        public string Title { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; }
    }
}