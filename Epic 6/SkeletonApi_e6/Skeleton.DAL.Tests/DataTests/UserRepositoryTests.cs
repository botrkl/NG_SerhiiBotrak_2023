using Skeleton.DAL.Context;
using Skeleton.DAL.Entities;
using Skeleton.DAL.Repositories;
using Skeleton.DAL.Tests.Data;
using Xunit;

namespace Skeleton.DAL.Tests.DataTests;

public class UserRepositoryTests
{
    [Theory]
    [InlineData("585dbcf1-2a39-4e24-8b66-2339ab6bdbab")]
    [InlineData("96865278-eff1-4be5-88a2-6d1272e2feeb")]
    public async Task UserRepository_GetByIdAsync_ReturnsValue(string id)
    {
        // arrange

        using var context = new QuizHubDatabaseContext(UnitTestHelper.GetUnitTestsDbOptions());
        var userRepository = new UserRepository(context);

        var expected = RepositoryData.ExpectedUsers.FirstOrDefault(x => x.Id.ToString() == id);

        // act

        var result = await userRepository.GetByIdAsync(Guid.Parse(id));

        // assert

        Assert.NotNull(result);
        Assert.Equal(expected, result, new UserEqualityComparer()!);
    }

    [Fact]
    public async Task UserRepository_GetAllAsync_ReturnsAllValues()
    {
        using var context = new QuizHubDatabaseContext(UnitTestHelper.GetUnitTestsDbOptions());
        var userRepository = new UserRepository(context);

        var result = await userRepository.GetAllAsync();

        Assert.Equal(RepositoryData.ExpectedUsers, result, new UserEqualityComparer());
    }

    [Fact]
    public async Task UserRepository_AddAsync_AddsValueToDatabase()
    {
        using var context = new QuizHubDatabaseContext(UnitTestHelper.GetUnitTestsDbOptions());
        var userRepository = new UserRepository(context);

        var newUser = new User { Name = "Name", Surname = "Surname", Password = "Pswrd" };

        await userRepository.AddAsync(newUser);
        await context.SaveChangesAsync();

        Assert.Equal(RepositoryData.ExpectedUsers.Count() + 1, context.Users.Count());
    }

    [Fact]
    public async Task UserRepository_UpdateAsync_ValueUpdated()
    {
        using var context = new QuizHubDatabaseContext(UnitTestHelper.GetUnitTestsDbOptions());
        var userRepository = new UserRepository(context);

        var userId = Guid.Parse("585dbcf1-2a39-4e24-8b66-2339ab6bdbab");
        var notExpected = RepositoryData.ExpectedUsers.FirstOrDefault(x => x.Id == userId);
        var user = new User
        {
            Id = userId,
            Name = "Name",
            Surname = "Surname",
            Password = "Pswrd"
        };

        await userRepository.UpdateAsync(user);
        await context.SaveChangesAsync();

        Assert.NotEqual(notExpected, user, new UserEqualityComparer()!);
    }

    [Theory]
    [InlineData("585dbcf1-2a39-4e24-8b66-2339ab6bdbab")]
    [InlineData("96865278-eff1-4be5-88a2-6d1272e2feeb")]
    public async Task UserRepository_DeleteAsync_ObjectIsDeleted(string id)
    {
        using var context = new QuizHubDatabaseContext(UnitTestHelper.GetUnitTestsDbOptions());

        var userRepository = new UserRepository(context);

        await userRepository.DeleteAsync(Guid.Parse(id));
        await context.SaveChangesAsync();

        Assert.Equal(RepositoryData.ExpectedUsers.Count() - 1, context.Users.Count());
    }

    [Fact]
    public async Task UserRepository_GetUserByCredentialsAsync_CredentialsAreCorrect_UserReturned()
    {
        using var context = new QuizHubDatabaseContext(UnitTestHelper.GetUnitTestsDbOptions());

        var userRepository = new UserRepository(context);

        var name = "Name2";
        var password = "drowssaP";

        var expected = RepositoryData.ExpectedUsers.FirstOrDefault(x => x.Name == name && x.Password == password);
        
        var result = await userRepository.GetUserByCredentialsAsync(name, password);

        Assert.Equal(expected, result , new UserEqualityComparer()!);
    }
}