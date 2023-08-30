using Skeleton.DAL.Context;
using Skeleton.DAL.Entities;
using Skeleton.DAL.Interfaces;

namespace Skeleton.DAL.Repositories;

public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
{
    public QuestionRepository(QuizHubDatabaseContext dbContext) : base(dbContext)
    {
    }
}