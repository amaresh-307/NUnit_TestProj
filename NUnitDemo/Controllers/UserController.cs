using Microsoft.AspNetCore.Mvc;
using NUnitDemo.Models;
using NUnitDemo.Repositories.Abstractions;

namespace NUnitDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<User> GetUser(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
                return NotFound();
            return user;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> AddUser([FromBody] User user)
        {
            _userRepository.AddUser(user);
            return CreatedAtAction(nameof(GetUser), new { userId = user.UserId }, user);
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUser(int userId)
        {
            _userRepository.DeleteUser(userId);
            return NoContent();
        }
    }
}
