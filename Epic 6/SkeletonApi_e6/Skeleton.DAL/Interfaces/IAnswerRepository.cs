using Skeleton.DAL.Entities;

namespace Skeleton.DAL.Interfaces;

public interface IAnswerRepository : IBaseRepository<Answer>
{
    Task<bool> CheckIsCorrectAsync(Guid id);
    Task<IEnumerable<Answer>> GetAllByQuestionIdAsync(Guid questionId);
}