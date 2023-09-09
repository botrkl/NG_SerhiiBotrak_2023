using AutoMapper;
using Skeleton.BLL.Exceptions;
using Skeleton.BLL.Interfaces;
using Skeleton.BLL.Models;
using Skeleton.BLL.Models.AddModels;
using Skeleton.BLL.Models.UpdateModels;
using Skeleton.DAL.Entities;
using Skeleton.DAL.Interfaces;
using Skeleton.DAL.Repositories;
using System.Xml.Linq;

namespace Skeleton.BLL.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserModel> GetUserAsync(Guid id)
    {
        var wantedUser = await _userRepository.GetByIdAsync(id);
        var mappedUser = _mapper.Map<UserModel>(wantedUser);
        return mappedUser;
    }
    public async Task AddUserAsync(AddUserModel userModel)
    {
        if (userModel.Name == "" || userModel.Surname == "" || userModel.Password == "")
        {
            throw new ModelIsEmptyException();
        }
        var addUser = _mapper.Map<User>(userModel);
        await _userRepository.AddAsync(addUser);
    }

    public async Task UpdateUserAsync(UpdateUserModel userModel)
    {
        if (userModel.Name == "" || userModel.Surname == "" || userModel.Password == "")
        {
            throw new ModelIsEmptyException();
        }        
        var tempUser = await _userRepository.GetByIdAsync(Guid.Parse(userModel.Id));
        _mapper.Map(userModel, tempUser);
        await _userRepository.UpdateAsync(tempUser);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        await _userRepository.DeleteAsync(id);
    }

    public async Task<UserModel?> GetUserByCredentialsAsync(string surname, string password)
    {
        var userList = await _userRepository.GetAllAsync();
        var user = userList.FirstOrDefault(x => x.Password == password && x.Surname == surname);
        return _mapper.Map<UserModel>(user);
    }
}