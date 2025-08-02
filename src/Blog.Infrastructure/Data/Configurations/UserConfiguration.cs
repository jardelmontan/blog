using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(m => m.Id)
                .HasColumnName("UserId");

            builder.Property(x => x.Name)
                .HasMaxLength(70)
                .IsUnicode(false);

            builder.Property(x => x.Role)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasConversion<string>();

            builder.Property(x => x.Email)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(x => x.PasswordHash)
                .HasMaxLength(256)
                .IsUnicode(false);

            builder.HasIndex(u => u.Email).IsUnique();
        }
    }
}
