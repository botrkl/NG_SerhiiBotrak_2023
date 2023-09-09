using System.Collections;
using AutoMapper;
using Skeleton.BLL.Interfaces;
using Skeleton.BLL.Models;
using Skeleton.BLL.Models.AddModels;
using Skeleton.DAL.Entities;
using Skeleton.DAL.Interfaces;

namespace Skeleton.BLL.Services;

public class AnswerService : IAnswerService
{
    private readonly IMapper _mapper;
    private readonly IAnswerRepository _answerRepository;

    public AnswerService(IAnswerRepository answerRepository, IMapper mapper)
    {
        _mapper = mapper;
        _answerRepository = answerRepository;
    }

    public async Task<IEnumerable<AnswerModel>> GetAnswersByQuestionIdAsync(Guid questionId)
    {
        var answers = await _answerRepository.GetAllByQuestionIdAsync(questionId);

        return _mapper.Map<IEnumerable<AnswerModel>>(answers);
    }

    public async Task<bool> CheckAnswerByIdAsync(Guid id)
    {
        return await _answerRepository.CheckIsCorrectAsync(id);
    }

    public async Task DeleteAnswerAsync(Guid id)
    {
        await _answerRepository.DeleteAsync(id);
    }

    public async Task AddAnswerAsync(AddAnswerModel model)
    {
        var addAnswer = _mapper.Map<Answer>(model);
        await _answerRepository.AddAsync(addAnswer);
    }

    public async Task UpdateAnswerAsync(UpdateAnswerModel model)
    {
        var tempAnswer = await _answerRepository.GetByIdAsync(Guid.Parse(model.Id));
        _mapper.Map(model, tempAnswer);
        await _answerRepository.UpdateAsync(tempAnswer);
    }
}