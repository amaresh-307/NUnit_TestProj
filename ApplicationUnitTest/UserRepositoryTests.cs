using Moq;
using NUnitDemo.Contexts;
using NUnitDemo.Models;
using NUnitDemo.Repositories.Implementations;

namespace ApplicationUnitTest
{
    internal class UserRepositoryTests
    {
        private Mock<IBankingContext> _mockContext;
        private IList<User> _users;
        private UserRepository _repository;

        [SetUp]
        public void Setup()
        {
            _users = new List<User>
            {
                new User { UserId = 1, Name = "John Doe" },
                new User { UserId = 2, Name = "Jane Smith" }
            };

            _mockContext = new Mock<IBankingContext>();
            _mockContext.Setup(c => c.Users).Returns(_users);

            _repository = new UserRepository(_mockContext.Object);
        }

        [Test]
        public void GetUserById_ValidId_ReturnsUser()
        {
            var result = _repository.GetUserById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.UserId, 1);
        }

        [Test]
        public void GetUserById_InvalidId_ReturnsNull()
        {
            var result = _repository.GetUserById(3);

            Assert.IsNull(result);
        }

        [Test]
        public void AddUser_ValidUser_AddsUser()
        {
            var newUser = new User { UserId = 3, Name = "Alice" };
            _repository.AddUser(newUser);

            Assert.Contains(newUser, _users.ToArray());
        }

        [Test]
        public void DeleteUser_ValidId_RemovesUser()
        {
            _repository.DeleteUser(1);

            var user = _users.SingleOrDefault(u => u.UserId == 1);
            Assert.IsNull(user);
        }
    }
}
