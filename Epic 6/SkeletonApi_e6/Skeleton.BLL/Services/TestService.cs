using AutoMapper;
using Skeleton.BLL.Interfaces;
using Skeleton.BLL.Models;
using Skeleton.BLL.Models.AddModels;
using Skeleton.DAL.Entities;
using Skeleton.DAL.Interfaces;

namespace Skeleton.BLL.Services;

public class TestService : ITestService
{
    private readonly ITestRepository _testRepository;
    private readonly IMapper _mapper;

    public TestService(ITestRepository testRepository, IMapper mapper)
    {
        _testRepository = testRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<string>> GetTestsByUserIdAsync(Guid userId)
    {
        var tests = await _testRepository.GetTestsByUserIdAsync(userId);
        var testId = tests.Select(test => test.Id.ToString());
        return testId;
    }

    public async Task<TestModel> GetTestWithQuestionsAsync(Guid id)
    {
        var wantedTest =  await _testRepository.GetByIdWithQuestionsAsync(id);
        var mappedTest =  _mapper.Map<TestModel>(wantedTest);
        return mappedTest;
    }

    public async Task<string> GetTestDescriptionAsync(Guid id)
    {
        var testDescription = await _testRepository.GetDescriptionAsync(id);
        return testDescription;
    }

    public async Task DeleteTestAsync(Guid id)
    {
        await _testRepository.DeleteAsync(id);
    }

    public async Task AddTestAsync(AddTestModel model)
    {
        var addTest = _mapper.Map<Test>(model);
        await _testRepository.AddAsync(addTest);
    }

    public async Task UpdateTestAsync(UpdateTestModel model)
    {
        var tempTest = await _testRepository.GetByIdAsync(Guid.Parse(model.Id));
        _mapper.Map(model, tempTest);
        await _testRepository.UpdateAsync(tempTest);
    }
}