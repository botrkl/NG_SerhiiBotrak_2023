using Skeleton.DAL.Context;
using Skeleton.DAL.Entities;
using Skeleton.DAL.Repositories;
using Skeleton.DAL.Tests.Data;
using Xunit;

namespace Skeleton.DAL.Tests.DataTests;

public class QuestionRepositoryTests
{
    [Theory]
    [InlineData("1969fe97-30b0-4f96-998a-bdc70d88d2cc")]
    [InlineData("a5d6c657-688b-4c0e-a96a-9a66cab342af")]
    public async Task QuestionRepository_GetByIdAsync_ReturnsValue(string id)
    {
        // arrange
        
        using var context = new QuizHubDatabaseContext(UnitTestHelper.GetUnitTestsDbOptions());
        var questionRepository = new QuestionRepository(context);

        var expected = RepositoryData.ExpectedQuestions.FirstOrDefault(x => x.Id.ToString() == id);

        // act

        var result = await questionRepository.GetByIdAsync(Guid.Parse(id));

        // assert
        
        Assert.NotNull(result);
        Assert.Equal(expected, result, new QuestionEqualityComparer()!);
    }

    [Fact]
    public async Task QuestionRepository_GetAllAsync_ReturnsAllValues()
    {
        using var context = new QuizHubDatabaseContext(UnitTestHelper.GetUnitTestsDbOptions());
        var questionRepository = new QuestionRepository(context);

        var result = await questionRepository.GetAllAsync();
        
        Assert.Equal(RepositoryData.ExpectedQuestions, result, new QuestionEqualityComparer());
    }

    [Fact]
    public async Task QuestionRepository_AddAsync_AddsValueToDatabase()
    {
        using var context = new QuizHubDatabaseContext(UnitTestHelper.GetUnitTestsDbOptions());
        var questionRepository = new QuestionRepository(context);

        var newQuestion = new Question { Text = "New question" };

        await questionRepository.AddAsync(newQuestion);
        await context.SaveChangesAsync();
        
        Assert.Equal(RepositoryData.ExpectedQuestions.Count() + 1, context.Questions.Count());
    }
    
    [Fact]
    public async Task QuestionRepository_UpdateAsync_ValueUpdated()
    {
        using var context = new QuizHubDatabaseContext(UnitTestHelper.GetUnitTestsDbOptions());
        var questionRepository = new QuestionRepository(context);

        var questionId = Guid.Parse("a5d6c657-688b-4c0e-a96a-9a66cab342af");
        var notExpected = RepositoryData.ExpectedQuestions.FirstOrDefault(x => x.Id == questionId);
        var question = new Question
        {
            Id = questionId,
            Text = "Update test"
        };

        await questionRepository.UpdateAsync(question);
        await context.SaveChangesAsync();
        
        Assert.NotEqual(notExpected, question, new QuestionEqualityComparer()!);
    }

    [Theory]
    [InlineData("a5d6c657-688b-4c0e-a96a-9a66cab342af")]
    [InlineData("ed20c0a6-dbc7-4f2f-9b4d-e57616f40c78")]
    public async Task QuestionRepository_DeleteAsync_ObjectIsDeleted(string id)
    {
        using var context = new QuizHubDatabaseContext(UnitTestHelper.GetUnitTestsDbOptions());

        var questionRepository = new QuestionRepository(context);

        await questionRepository.DeleteAsync(Guid.Parse(id));
        await context.SaveChangesAsync();

        Assert.Equal(RepositoryData.ExpectedQuestions.Count() - 1, context.Questions.Count());
    }
}