using NUnitDemo.Models;

namespace NUnitDemo.Contexts
{

    public interface IBankingContext
    {
        IList<User> Users { get; set; }
        IList<UserAccount> UserAccounts { get; set; }
    }

    public class BankingContext : IBankingContext
    {
        public BankingContext()
        {
             Users = new List<User>();
             UserAccounts = new List<UserAccount>();
        }
        public IList<User> Users { get; set; }
        public IList<UserAccount> UserAccounts { get; set; }
    }
}
