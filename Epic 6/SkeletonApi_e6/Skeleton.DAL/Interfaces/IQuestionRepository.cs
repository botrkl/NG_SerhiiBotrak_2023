using Skeleton.DAL.Entities;

namespace Skeleton.DAL.Interfaces;

public interface IQuestionRepository : IBaseRepository<Question>
{
    Task<IEnumerable<Question>> GetAllByTestIdAsync(Guid id);
}