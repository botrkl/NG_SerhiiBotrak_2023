using Microsoft.EntityFrameworkCore;
using Skeleton.DAL.Context;
using Skeleton.DAL.Entities;
using Skeleton.DAL.Interfaces;

namespace Skeleton.DAL.Repositories;

public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
{
    public QuestionRepository(QuizHubDatabaseContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Question>> GetAllByTestIdAsync(Guid id)
    {
        var searchedList = await _dbContext.Set<Question>()
            .Include(x=>x.TestId)
            .Where(x=>x.Test !=null && x.TestId == id)
            .ToListAsync();
        return searchedList;
    }
}