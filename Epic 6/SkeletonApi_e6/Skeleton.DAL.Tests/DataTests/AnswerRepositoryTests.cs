using Skeleton.DAL.Context;
using Skeleton.DAL.Entities;
using Skeleton.DAL.Repositories;
using Skeleton.DAL.Tests.Data;
using Xunit;

namespace Skeleton.DAL.Tests.DataTests;

public class AnswerRepositoryTests
{
    [Theory]
    [InlineData("d8d06d13-6a7e-417c-bbdb-76f9c6a1cfab")]
    [InlineData("6225b805-399f-47eb-8b37-b4ed4c286914")]
    public async Task AnswerRepository_GetByIdAsync_ReturnsValue(string id)
    {
        // arrange
        
        using var context = new QuizHubDatabaseContext(UnitTestHelper.GetUnitTestsDbOptions());
        var answerRepository = new AnswerRepository(context);

        var expected = RepositoryData.ExpectedAnswers.FirstOrDefault(x => x.Id.ToString() == id);

        // act

        var result = await answerRepository.GetByIdAsync(Guid.Parse(id));

        // assert
        
        Assert.NotNull(result);
        Assert.Equal( expected, result, new AnswerEqualityComparer()!);
    }

    [Fact]
    public async Task AnswerRepository_GetAllAsync_ReturnsAllValues()
    {
        using var context = new QuizHubDatabaseContext(UnitTestHelper.GetUnitTestsDbOptions());
        var answerRepository = new AnswerRepository(context);

        var result = await answerRepository.GetAllAsync();
        
        Assert.Equal(RepositoryData.ExpectedAnswers, result, new AnswerEqualityComparer());
    }

    [Fact]
    public async Task AnswerRepository_AddAsync_AddsValueToDatabase()
    {
        using var context = new QuizHubDatabaseContext(UnitTestHelper.GetUnitTestsDbOptions());
        var answerRepository = new AnswerRepository(context);

        var newAnswer = new Answer { Text = "New answer" };

        await answerRepository.AddAsync(newAnswer);
        await context.SaveChangesAsync();
        
        Assert.Equal(RepositoryData.ExpectedAnswers.Count() + 1, context.Answers.Count());
    }
    
    [Fact]
    public async Task AnswerRepository_UpdateAsync_ValueUpdated()
    {
        using var context = new QuizHubDatabaseContext(UnitTestHelper.GetUnitTestsDbOptions());
        var answerRepository = new AnswerRepository(context);

        var answerId = Guid.Parse("6225b805-399f-47eb-8b37-b4ed4c286914");
        var notExpected = RepositoryData.ExpectedAnswers.FirstOrDefault(x => x.Id == answerId);
        var answer = new Answer
        {
            Id = answerId,
            Text = "Update test"
        };

        await answerRepository.UpdateAsync(answer);
        await context.SaveChangesAsync();
        
        Assert.NotEqual(notExpected, answer, new AnswerEqualityComparer()!);
    }

    [Theory]
    [InlineData("6225b805-399f-47eb-8b37-b4ed4c286914")]
    [InlineData("b1a30461-2173-416b-8281-a227c5cff8f7")]
    public async Task AnswerRepository_DeleteAsync_ObjectIsDeleted(string id)
    {
        using var context = new QuizHubDatabaseContext(UnitTestHelper.GetUnitTestsDbOptions());

        var answerRepository = new AnswerRepository(context);

        await answerRepository.DeleteAsync(Guid.Parse(id));
        await context.SaveChangesAsync();

        Assert.Equal(RepositoryData.ExpectedAnswers.Count() - 1, context.Answers.Count());
    }

    [Theory]
    [InlineData("aa6cc972-2650-44c3-8a8e-134e4e7cf8df")]
    [InlineData("a5d6c657-688b-4c0e-a96a-9a66cab342af")]
    public async Task AnswerRepository_GetAllByQuestionIdAsync_AnswersAreReturned(string id)
    {
        using var context = new QuizHubDatabaseContext(UnitTestHelper.GetUnitTestsDbOptions());

        var answerRepository = new AnswerRepository(context);

        var questionId = Guid.Parse(id);

        var answerCount = RepositoryData.ExpectedAnswers.Count(x => x.QuestionId == questionId);

        var result = await answerRepository.GetAllByQuestionIdAsync(questionId);

        Assert.Equal(answerCount, result.Count());
    }
}