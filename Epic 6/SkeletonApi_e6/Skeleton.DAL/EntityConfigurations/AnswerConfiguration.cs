using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skeleton.DAL.Entities;

namespace Skeleton.DAL.EntityConfigurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Text).IsRequired();
            builder.Property(x=>x.IsCorrect).IsRequired();
        }
    }
}
