namespace Skeleton.BLL.Interfaces;

public interface IAuthenticationService
{
    public Task<Guid> AuthenticateAsync(string surname, string password);
}