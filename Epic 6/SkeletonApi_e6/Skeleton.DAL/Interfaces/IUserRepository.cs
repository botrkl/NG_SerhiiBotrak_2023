using Skeleton.DAL.Entities;

namespace Skeleton.DAL.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetUserByCredentialsAsync(string name, string password);
}