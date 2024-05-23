using Moq;
using NUnitDemo.Contexts;
using NUnitDemo.Models;
using NUnitDemo.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationUnitTest
{
    internal class UserAccountRepositoryTests
    {
        private Mock<IBankingContext> _mockContext;
        private IList<UserAccount> _userAccounts;
        private UserAccountRepository _repository;
     
        [SetUp]
        public void Setup()
        {
            _userAccounts = new List<UserAccount>
            {
                new UserAccount { AccountId = 1, Balance = 500 },
                new UserAccount { AccountId = 2, Balance = 1500 }
            };

            _mockContext = new Mock<IBankingContext>();

            _mockContext.Setup(c => c.UserAccounts).Returns(_userAccounts);

            _repository = new UserAccountRepository(_mockContext.Object);
        }

        [Test]
        public void GetUserAccountById_ValidId_ReturnsUserAccount()
        {
            var result = _repository.GetUserAccountById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.AccountId);
        }

        [Test]
        public void GetUserAccountById_InvalidId_ReturnsNull()
        {
            var result = _repository.GetUserAccountById(3);

            Assert.IsNull(result);
        }

        [Test]
        public void AddUserAccount_ValidAccount_AddsAccount()
        {
            var newAccount = new UserAccount { AccountId = 3, Balance = 1000 };
            _repository.AddUserAccount(newAccount);

            Assert.Contains(newAccount, _userAccounts.ToArray());
        }

        [Test]
        public void Withdraw_ValidAmount_UpdatesBalance()
        {
            _repository.Withdraw(1, 200);

            var account = _userAccounts.Single(a => a.AccountId == 1);
            Assert.AreEqual(300, account.Balance);
        }

        [Test]
        public void Withdraw_MoreThan90Percent_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => _repository.Withdraw(1, 451));
        }

        [Test]
        public void Withdraw_LessThanMinimumBalance_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => _repository.Withdraw(1, 450));
        }

        [Test]
        public void Deposit_ValidAmount_UpdatesBalance()
        {
            _repository.Deposit(1, 500);

            var account = _userAccounts.Single(a => a.AccountId == 1);
            Assert.AreEqual(1000, account.Balance);
        }

        [Test]
        public void Deposit_MoreThanLimit_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => _repository.Deposit(1, 10001));
        }

        [Test]
        public void DeleteUserAccount_ValidId_RemovesAccount()
        {
            _repository.DeleteUserAccount(1);

            var account = _userAccounts.SingleOrDefault(a => a.AccountId == 1);
            Assert.IsNull(account);
        }
    }
}
