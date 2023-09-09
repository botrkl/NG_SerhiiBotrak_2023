using Skeleton.BLL.Models;
using Skeleton.BLL.Models.AddModels;
using Skeleton.BLL.Models.UpdateModels;
using Skeleton.DAL.Entities;

namespace Skeleton.BLL.Interfaces;

public interface IUserService
{
    public Task AddUserAsync(AddUserModel userModel);
    public Task UpdateUserAsync(UpdateUserModel userModel);
    public Task DeleteUserAsync(Guid id);
    public Task<UserModel> GetUserAsync(Guid id);

    public Task<UserModel?> GetUserByCredentialsAsync(string surname, string password);
}