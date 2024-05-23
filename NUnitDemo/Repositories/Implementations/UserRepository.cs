using NUnitDemo.Contexts;
using NUnitDemo.Models;
using NUnitDemo.Repositories.Abstractions;

namespace NUnitDemo.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IBankingContext _context;

        public UserRepository(IBankingContext context)
        {
            _context = context;
        }

        public User GetUserById(int userId) => _context.Users.SingleOrDefault(u => u.UserId == userId);
        public void AddUser(User user)
        { 
            _context.Users.Add(user); 
        }

        public void DeleteUser(int userId)
        {
            var user = GetUserById( userId);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
        }
    }
}
