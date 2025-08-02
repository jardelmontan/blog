namespace Blog.Domain.Entities
{
    public sealed class Post : BaseEntity
    {
        public required string Title { get; set; }
        public required string Content { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
