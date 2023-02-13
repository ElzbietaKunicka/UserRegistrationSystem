using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserRegistrationSystem.BLL;
using UserRegistrationSystem.Dto;

namespace UserRegistrationSystem.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IJwtService _jwtService;
        public AccountsController(IAccountService accountService, IJwtService jwtService)
        {
            _accountService = accountService;
            _jwtService = jwtService;
        }

        [HttpPost("SignUp")]
        public ActionResult Signup([FromBody] AuthRequestDto request)
        {
            _accountService.SignupNewAccount(request.UserName, request.Password);
            return Ok();
        }

        [HttpPost("Login")]
        public ActionResult Login([FromBody] AuthRequestDto request)
        {
            var (loginSuccess, account) = _accountService.Login(request.UserName, request.Password);
            if (!loginSuccess)
            {
                return BadRequest("Invalid username or password");
            }
            else
            {
                var jwt = _jwtService.GetJwtToken(account.UserName, account.Id, account.Role);
                // todo generate jwt
                return Ok(jwt);
            }
        }
    }
}
