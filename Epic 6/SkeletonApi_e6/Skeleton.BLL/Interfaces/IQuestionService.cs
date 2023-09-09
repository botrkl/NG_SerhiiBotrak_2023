using Skeleton.BLL.Models;
using Skeleton.BLL.Models.AddModels;
using Skeleton.DAL.Entities;

namespace Skeleton.BLL.Interfaces;

public interface IQuestionService
{
    public Task<IEnumerable<QuestionModel>> GetQuestionsByTestIdAsync(Guid testId);

    public Task DeleteQuestionAsync(Guid id);
    public Task AddQuestionAsync(AddQuestionModel model);
    public Task UpdateQuestionAsync(UpdateQuestionModel model);
}