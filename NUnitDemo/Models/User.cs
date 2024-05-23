using System.Security.Principal;

namespace NUnitDemo.Models
{
    public class User
    {
        public User()
        {
            UserAccounts = new List<UserAccount>();
            Name = string.Empty;
        }
        public int UserId { get; set; }
        public string Name { get; set; }
        public ICollection<UserAccount> UserAccounts { get; set; }
    }


}
