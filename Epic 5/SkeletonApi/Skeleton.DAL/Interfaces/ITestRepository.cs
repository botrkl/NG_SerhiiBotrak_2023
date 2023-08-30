using Skeleton.DAL.Entities;

namespace Skeleton.DAL.Interfaces;

public interface ITestRepository : IBaseRepository<Test>
{
    Task<IEnumerable<Test>> GetTestsByUserIdAsync(Guid userId);
    Task<Test> GetByIdWithQuestionsAsync(Guid id);
    Task<string> GetDescriptionAsync(Guid id);
}