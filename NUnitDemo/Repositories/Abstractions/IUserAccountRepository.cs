using NUnitDemo.Models;

namespace NUnitDemo.Repositories.Abstractions
{
    public interface IUserAccountRepository
    {
        UserAccount GetUserAccountById(int accountId);
        void AddUserAccount(UserAccount account);
        void Deposit(int accountId, decimal amount);
        void Withdraw(int accountId, decimal amount);
        void DeleteUserAccount(int accountId);

    }
}
