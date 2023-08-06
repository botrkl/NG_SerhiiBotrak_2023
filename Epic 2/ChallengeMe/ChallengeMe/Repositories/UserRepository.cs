using ChallengeMe.Entities;
using ChallengeMe.Repositories.Interface;

namespace ChallengeMe.Repositories
{
    public class UserRepository : IUserRepository
    {
        private List<User> _users;

        public UserRepository()
        {
            _users = new List<User>();
        }

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public void RemoveUser(string username)
        {
            var userToRemove = _users.FirstOrDefault(user => user.Username == username);
            if (userToRemove != null)
            {
                _users.Remove(userToRemove);
            }
        }

        public bool UserExists(string username, string password)
        {
            return _users.Any(user => user.Username == username && user.Password == password);
        }
    }
}
