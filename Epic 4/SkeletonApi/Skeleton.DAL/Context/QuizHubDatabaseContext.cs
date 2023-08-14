using Microsoft.EntityFrameworkCore;
using Skeleton.DAL.Entities;
using Skeleton.DAL.EntityConfigurations;

namespace Skeleton.DAL.Context
{
    public class QuizHubDatabaseContext:DbContext
    {
        public QuizHubDatabaseContext(DbContextOptions options) : base(options) { }

        DbSet<User> Users { get; set; }
        DbSet<Test> Tests { get; set; }
        DbSet<Question> Questions { get; set; }
        DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TestConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
            modelBuilder.ApplyConfiguration(new AnswerConfiguration());

            base.OnModelCreating(modelBuilder);
        }

    }
}
