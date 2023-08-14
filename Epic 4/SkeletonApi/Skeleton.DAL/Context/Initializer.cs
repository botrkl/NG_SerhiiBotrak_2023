
namespace Skeleton.DAL.Context
{
    public static class Initializer
    {
        public static void Initialize(QuizHubDatabaseContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
