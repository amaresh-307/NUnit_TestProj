using NUnitDemo.Models;

namespace NUnitDemo.Repositories.Abstractions
{
    public interface IUserRepository
    {
        User GetUserById(int userId);
        void AddUser(User user);
        void DeleteUser(int userId);
    }
}
