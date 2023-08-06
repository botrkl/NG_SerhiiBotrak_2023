using ChallengeMe.Entities;
using ChallengeMe.Repositories.Interface;
using ChallengeMe.Services.Interface;

namespace ChallengeMe.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void EnterTheFridge(string username, string password)
        {
            bool isHuman = _userRepository.UserExists(username, password);

            if (isHuman)
            {
                Console.WriteLine("You have entered the fridge");
            }
            else
            {
                Console.WriteLine("Your fridge has benn lohed");
            }
        }

        public void RegisterUser(string username, string password)
        {
            if (username == null)
            {
                throw new ArgumentNullException("Username must not be null",nameof(username));
            }
            if(password == null)
            {
                throw new ArgumentNullException("Password must not be null", nameof(password));
            }

            _userRepository.AddUser(new User(username, password));
        }

        public void UnregisterUser(string username)
        {
            _userRepository.RemoveUser(username);
        }
    }
}
