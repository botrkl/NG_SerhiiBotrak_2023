using Microsoft.EntityFrameworkCore;
using Skeleton.DAL.Context;
using Skeleton.DAL.Entities;
using Skeleton.DAL.Interfaces;

namespace Skeleton.DAL.Repositories;

public class TestRepository : BaseRepository<Test>, ITestRepository
{
    public TestRepository(QuizHubDatabaseContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Test>> GetTestsByUserIdAsync(Guid userId)
    {
        var searchedList = await _dbContext.Set<Test>()
            .Include(x => x.User)
            .Where(x=> x.User != null && x.User.Id == userId)
            .ToListAsync();
        return searchedList;
    }

    public async  Task<Test> GetByIdWithQuestionsAsync(Guid id)
    {
        var searchedEntity = await _dbContext.Set<Test>().Include(x => x.Questions).FirstOrDefaultAsync(x=>x.Id==id);
        if(searchedEntity is null)
        {
            throw new ArgumentNullException($"{nameof(searchedEntity)} with this id not found.", nameof(AnswerRepository));
        }
        return searchedEntity;
    }

    public async Task<string> GetDescriptionAsync(Guid id)
    {
        var searchedEntity = await base.GetByIdAsync(id);
        if (searchedEntity is null)
        {
            throw new ArgumentNullException($"{nameof(searchedEntity)} with this id not found.", nameof(TestRepository));
        }
        return searchedEntity.Description;
    }
}