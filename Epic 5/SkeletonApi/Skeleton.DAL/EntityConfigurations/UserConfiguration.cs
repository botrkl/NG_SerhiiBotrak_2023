using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skeleton.DAL.Entities;

namespace Skeleton.DAL.EntityConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(25);
            builder.Property(x=>x.Surname).IsRequired().HasMaxLength(25);
            builder.Property(x => x.Password).IsRequired();

            builder
                .HasMany(x => x.Tests)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.CreatedForUserId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
