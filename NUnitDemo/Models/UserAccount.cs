namespace NUnitDemo.Models
{
    public class UserAccount
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public decimal Balance { get; set; }
        public User User { get; set; }
    }
}
