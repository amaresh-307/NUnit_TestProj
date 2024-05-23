using NUnitDemo.Contexts;
using NUnitDemo.Models;
using NUnitDemo.Repositories.Abstractions;

namespace NUnitDemo.Repositories.Implementations
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly IBankingContext _context;

        public UserAccountRepository(IBankingContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets UserAccount by Id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns>UserAccount</returns>
        public UserAccount GetUserAccountById(int accountId)
        {
            return _context.UserAccounts.SingleOrDefault(a => a.AccountId == accountId);
        }

        public void AddUserAccount(UserAccount account)
        {
            _context.UserAccounts.Add(account);
        }

        public void Withdraw(int accountId, decimal amount)
        {
            var account = GetUserAccountById(accountId);

            //an user cannot withdraw more than 90% of their total balance from an account in a single transaction.
            if (amount > 0.9m * account.Balance)
                throw new InvalidOperationException("Limit exceeded... Cannot withdraw more than 90% of the total balance in a single transaction.");

            //an account cannot have less than $100 at any time in an account.
            if (account.Balance - amount < 100)
                throw new InvalidOperationException("Not enough Money...Account balance cannot be less than $100.");

            account.Balance -= amount;
        }


        // an user cannot deposit more than $10,000 in a single transaction.
        public void Deposit(int accountId, decimal amount)
        {
            if (amount > 10000)
                throw new InvalidOperationException("Limit exceeded... Cannot deposit more than $10,000 in a single transaction.");

            var account = GetUserAccountById(accountId);
            account.Balance += amount;
        }

        public void DeleteUserAccount(int accountId)
        {
            var account = _context.UserAccounts.FirstOrDefault(a => a.AccountId == accountId);
            if (account != null)
            {
                _context.UserAccounts.Remove(account);
            }

        }
    }
}
