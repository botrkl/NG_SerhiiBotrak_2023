using Skeleton.BLL.Models;
using Skeleton.BLL.Models.AddModels;

namespace Skeleton.BLL.Interfaces;

public interface IAnswerService
{
    public Task<IEnumerable<AnswerModel>> GetAnswersByQuestionIdAsync(Guid questionId);
    public Task<bool> CheckAnswerByIdAsync(Guid id);

    public Task DeleteAnswerAsync(Guid id);
    public Task AddAnswerAsync(AddAnswerModel model);
    public Task UpdateAnswerAsync(UpdateAnswerModel model);
}