using ConferenceCheckInSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceCheckInSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new()
        {
            new User { Id = 1, FirstName = "Admin", LastName = "Staff", Email = "admin@event.com", Username = "admin", Password = "password123" }
        };

        public User? GetByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.Username.Equals(username, System.StringComparison.OrdinalIgnoreCase));
        }

        public bool ValidateUser(string username, string password)
        {
            return _users.Any(u => u.Username.Equals(username, System.StringComparison.OrdinalIgnoreCase) && u.Password == password);
        }

        public void AddUser(User user)
        {
            user.Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;
            _users.Add(user);
        }
    }
}