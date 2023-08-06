
namespace ChallengeMe.Services.Interface
{
    public interface IUserService
    {
        void RegisterUser(string username, string password);
        void EnterTheFridge(string username, string password);
        void UnregisterUser(string username);
    }
}
