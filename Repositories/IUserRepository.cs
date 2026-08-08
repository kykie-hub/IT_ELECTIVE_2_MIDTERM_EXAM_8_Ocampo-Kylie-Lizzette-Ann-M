using ConferenceCheckInSystem.Models;

namespace ConferenceCheckInSystem.Repositories
{
    public interface IUserRepository
    {
        User? GetByUsername(string username);
        bool ValidateUser(string username, string password);
        void AddUser(User user);
    }
}