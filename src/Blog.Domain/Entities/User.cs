using Blog.Domain.Enums;

namespace Blog.Domain.Entities
{
    public sealed class User : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public UserRole Role { get; set; }

        public ICollection<Post> Posts { get; set; } = [];
    }
}
