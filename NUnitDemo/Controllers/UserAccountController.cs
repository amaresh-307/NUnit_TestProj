using Microsoft.AspNetCore.Mvc;
using NUnitDemo.Models;
using NUnitDemo.Repositories.Abstractions;

namespace NUnitDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountRepository _accountRepository;

        public UserAccountController(IUserAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet("{accountId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserAccount> GetAccount(int accountId)
        {
            var account = _accountRepository.GetUserAccountById(accountId);
            if (account == null)
                return NotFound();
            return account;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserAccount> AddAccount([FromBody] UserAccount account)
        {
            _accountRepository.AddUserAccount(account);
            return CreatedAtAction(nameof(GetAccount), new { accountId = account.AccountId }, account);
        }

        [HttpPost("{accountId}/deposit")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Deposit(int accountId, [FromBody] decimal amount)
        {
            var account = _accountRepository.GetUserAccountById(accountId);
            if (account == null)
                return NotFound();

            try
            {
                _accountRepository.Deposit(accountId, amount);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{accountId}/withdraw")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Withdraw(int accountId, [FromBody] decimal amount)
        {
            var account = _accountRepository.GetUserAccountById(accountId);
            if (account == null)
                return NotFound();
            try
            {
                _accountRepository.Withdraw(accountId, amount);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{accountId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteAccount(int accountId)
        {
            _accountRepository.DeleteUserAccount(accountId);
            return NoContent();
        }

    }
}
