using Moq;
using Skeleton.DAL.Context;
using Skeleton.DAL.Entities;
using Skeleton.DAL.Repositories;
using Skeleton.DAL.Tests.Data;
using Xunit;

namespace Skeleton.DAL.Tests.DataTests;

public class TestRepositoryTests
{
    [Theory]
    [InlineData("f626f7b9-b1c0-4c0c-90c6-4246aced0c22")]
    [InlineData("c9e3368b-601a-4f9a-b4f2-e2e207de3b54")]
    public async Task TestRepository_GetByIdAsync_ReturnsValue(string id)
    {
        // arrange

        using var context = new QuizHubDatabaseContext(UnitTestHelper.GetUnitTestsDbOptions());
        var testRepository = new TestRepository(context);

        var expected = RepositoryData.ExpectedTests.FirstOrDefault(x => x.Id.ToString() == id);

        // act

        var result = await testRepository.GetByIdAsync(Guid.Parse(id));

        // assert

        Assert.NotNull(result);
        Assert.Equal(expected, result,new TestEqualityComparer()!);
    }

    [Fact]
    public async Task TestRepository_GetAllAsync_ReturnsAllValues()
    {
        using var context = new QuizHubDatabaseContext(UnitTestHelper.GetUnitTestsDbOptions());
        var testRepository = new TestRepository(context);

        var result = await testRepository.GetAllAsync();

        Assert.Equal(RepositoryData.ExpectedTests, result, new TestEqualityComparer());
    }

    [Fact]
    public async Task TestRepository_AddAsync_AddsValueToDatabase()
    {
        using var context = new QuizHubDatabaseContext(UnitTestHelper.GetUnitTestsDbOptions());
        var testRepository = new TestRepository(context);

        var newTest = new Test { Title = "New test", Description = "Some test" };

        await testRepository.AddAsync(newTest);
        await context.SaveChangesAsync();

        Assert.Equal(4, context.Tests.Count());
    }

    [Fact]
    public async Task TestRepository_UpdateAsync_ValueUpdated()
    {
        using var context = new QuizHubDatabaseContext(UnitTestHelper.GetUnitTestsDbOptions());
        var testRepository = new TestRepository(context);

        var testId = Guid.Parse("f626f7b9-b1c0-4c0c-90c6-4246aced0c22");
        var notExpected = RepositoryData.ExpectedTests.FirstOrDefault(x => x.Id == testId);
        var test = new Test
        {
            Id = testId,
            Title = "new title",
            Description = "new description"
        };

        await testRepository.UpdateAsync(test);
        await context.SaveChangesAsync();

        Assert.NotEqual(notExpected, test, new TestEqualityComparer()!);
    }

    [Theory]
    [InlineData("f626f7b9-b1c0-4c0c-90c6-4246aced0c22")]
    [InlineData("c9e3368b-601a-4f9a-b4f2-e2e207de3b54")]
    public async Task TestRepository_DeleteAsync_ObjectIsDeleted(string id)
    {
        using var context = new QuizHubDatabaseContext(UnitTestHelper.GetUnitTestsDbOptions());

        var testRepository = new TestRepository(context);

        await testRepository.DeleteAsync(Guid.Parse(id));
        await context.SaveChangesAsync();

        Assert.NotEqual(RepositoryData.ExpectedTests.Count(), context.Tests.Count());
    }

    [Theory]
    [InlineData("f626f7b9-b1c0-4c0c-90c6-4246aced0c22")]
    [InlineData("c9e3368b-601a-4f9a-b4f2-e2e207de3b54")]
    public async Task TestRepository_GetByIdWithQuestionsAsync_TestWithQuestionsReturned(string id)
    {
        using var context = new QuizHubDatabaseContext(UnitTestHelper.GetUnitTestsDbOptions());

        var testRepository = new TestRepository(context);

        var testId = Guid.Parse(id);

        var questionCount = RepositoryData.ExpectedQuestions.Count(x => x.TestId == testId);

        var result = await testRepository.GetByIdWithQuestionsAsync(testId);

        Assert.Equal(questionCount, result.Questions.Count);
    }

    [Theory]
    [InlineData("f626f7b9-b1c0-4c0c-90c6-4246aced0c22")]
    [InlineData("c9e3368b-601a-4f9a-b4f2-e2e207de3b54")]
    public async Task TestRepository_GetDescriptionAsync_ReturnedDescriptionIsCorrect(string id)
    {
        using var context = new QuizHubDatabaseContext(UnitTestHelper.GetUnitTestsDbOptions());

        var testRepository = new TestRepository(context);

        var testId = Guid.Parse(id);
        var expectedDescription = RepositoryData.ExpectedTests.Where(x => x.Id == testId).Select(x => x.Description).FirstOrDefault();

        var description = await testRepository.GetDescriptionAsync(testId);

        Assert.Equal(expectedDescription, description);
    }

    [Theory]
    [InlineData("585dbcf1-2a39-4e24-8b66-2339ab6bdbab")]
    [InlineData("96865278-eff1-4be5-88a2-6d1272e2feeb")]
    public async Task TestRepository_GetTestsByUserIdAsync_TestsAreReturned(string id)
    {
        using var context = new QuizHubDatabaseContext(UnitTestHelper.GetUnitTestsDbOptions());

        var testRepository = new TestRepository(context);

        var userId = Guid.Parse(id);

        var testCount = RepositoryData.ExpectedTests.Count(x => x.CreatedForUserId == userId);

        var result = await testRepository.GetTestsByUserIdAsync(userId);

        Assert.Equal(testCount, result.Count());
    }
}