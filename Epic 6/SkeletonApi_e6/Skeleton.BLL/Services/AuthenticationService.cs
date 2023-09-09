using Skeleton.BLL.Exceptions;
using Skeleton.BLL.Interfaces;
using Skeleton.DAL.Interfaces;

namespace Skeleton.BLL.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Guid> AuthenticateAsync(string surname, string password)
    {
        var user = await _userRepository.GetUserByCredentialsAsync(surname, password);
        if(user is null)
        {
            throw new AuthException();
        }
        return Guid.Parse(user.Id.ToString());
    }
}