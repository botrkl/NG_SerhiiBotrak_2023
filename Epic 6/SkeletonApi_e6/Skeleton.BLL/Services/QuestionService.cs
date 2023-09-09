using AutoMapper;
using Skeleton.BLL.Interfaces;
using Skeleton.BLL.Models;
using Skeleton.BLL.Models.AddModels;
using Skeleton.DAL.Entities;
using Skeleton.DAL.Interfaces;
using Skeleton.DAL.Repositories;
using System.Collections.Generic;

namespace Skeleton.BLL.Services;

public class QuestionService : IQuestionService
{
    private readonly IMapper _mapper;
    private readonly IQuestionRepository _questionRepository;

    public QuestionService(IQuestionRepository questionRepository, IMapper mapper)
    {
        _mapper = mapper;
        _questionRepository = questionRepository;
    }

    public async Task<IEnumerable<QuestionModel>> GetQuestionsByTestIdAsync(Guid testId)
    {
        var questionsList = await _questionRepository.GetAllByTestIdAsync(testId);
        var resultList = _mapper.Map<IEnumerable<QuestionModel>>(questionsList);
        return resultList;
    }
    public async Task AddQuestionAsync(AddQuestionModel model)
    {
        var addQuestion = _mapper.Map<Question>(model);
        await _questionRepository.AddAsync(addQuestion);
    }

    public async Task DeleteQuestionAsync(Guid id)
    {
        await _questionRepository.DeleteAsync(id);
    }

    public async Task UpdateQuestionAsync(UpdateQuestionModel model)
    {
        var tempQuestion = await _questionRepository.GetByIdAsync(Guid.Parse(model.Id));
        _mapper.Map(model, tempQuestion);
        await _questionRepository.UpdateAsync(tempQuestion);
    }
}