using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserRegistrationSystem.BLL;
using UserRegistrationSystem.DAL;
using UserRegistrationSystem.Models;

namespace UserRegistrationSystem.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IJwtService _jwtService;
        public AccountsController(IAccountService accountService,
            IJwtService jwtService)
        {
            _accountService = accountService;
            _jwtService = jwtService;
        }

        [HttpPost("SignUp")]
        public ActionResult Signup([FromBody] AuthRequestDto request)
        {
            try
            {
                if (request.UserName.Length > 25 || request.UserName.Length < 3)
                {
                    return BadRequest("UserName cannot be greater than 25 or less than 3");
                }
                if (request.Password.Length < 3)
                {
                    return BadRequest("Password cannot be greater than 50 or less than 3");
                }
                _accountService.SignupNewAccount(request.UserName,
                    request.Password);
                return Content($"{request.UserName}");
            }
            catch (Exception ex)
            {
                return BadRequest("A user with this username already exists.");
            }
        }

        [HttpPost("Login")]
        public ActionResult Login([FromBody] AuthRequestDto request)
        {
            var (loginSuccess, account) = _accountService
                .Login(request.UserName, request.Password);
            if (!loginSuccess)
            {
                return BadRequest("Invalid username or password");
            }
            else
            {
                var jwt = _jwtService.GetJwtToken(account.UserName, 
                    account.Id, account.Role);
                return Ok(jwt);
            }
        }
    }
}
