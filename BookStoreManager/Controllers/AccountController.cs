using BookStoreManager.Models;
using BookStoreManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreManager.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            _accountService.RegisterUser(registerUserDto);

            return Ok();
        }
    }
}
