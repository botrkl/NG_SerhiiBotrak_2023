using Skeleton.BLL.Models;
using Skeleton.BLL.Models.AddModels;

namespace Skeleton.BLL.Interfaces;

public interface ITestService
{
    public Task<IEnumerable<string>> GetTestsByUserIdAsync(Guid userId);
    public Task<TestModel> GetTestWithQuestionsAsync(Guid id);
    public Task<string> GetTestDescriptionAsync(Guid id);

    public Task DeleteTestAsync(Guid id);
    public Task AddTestAsync(AddTestModel model);
    public Task UpdateTestAsync(UpdateTestModel model);
}