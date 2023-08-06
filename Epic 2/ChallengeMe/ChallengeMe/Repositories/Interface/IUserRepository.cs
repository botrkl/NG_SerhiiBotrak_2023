using ChallengeMe.Entities;

namespace ChallengeMe.Repositories.Interface
{
    public interface IUserRepository
    {
        void AddUser(User user);
        void RemoveUser(string username);
        bool UserExists(string username, string password);
    }
}
