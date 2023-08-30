using Microsoft.EntityFrameworkCore;
using Skeleton.DAL.Context;
using Skeleton.DAL.Entities;
using Skeleton.DAL.Interfaces;

namespace Skeleton.DAL.Repositories;

public class AnswerRepository : BaseRepository<Answer>, IAnswerRepository
{
    public AnswerRepository(QuizHubDatabaseContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> CheckIsCorrectAsync(Guid id)
    {
        var searchedEntity = await base.GetByIdAsync(id);
        if(searchedEntity is null)
        {
            throw new ArgumentNullException($"{nameof(searchedEntity)} with this id not found.", nameof(AnswerRepository));
        }
        return searchedEntity.IsCorrect;
    }

    public async Task<IEnumerable<Answer>> GetAllByQuestionIdAsync(Guid questionId)
    {
        var searchedList = await _dbContext.Set<Answer>()
            .Include(x => x.Question)
            .Where(x => x.Question != null && x.Question.Id == questionId)
            .ToListAsync();
        return searchedList;
    }
}