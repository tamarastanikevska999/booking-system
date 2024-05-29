using Core.DomainModels;
using Core.DTO;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public AccountController(ITokenService tokenService)
        {

            _tokenService = tokenService;

        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] User login)
        {
            if (login.Username == "test" && login.Password == "password")
            {
                return Ok(new UserDto
                {
                    Token = _tokenService.CreateToken(login),
                    Username = login.Username
                });
            }

            return Unauthorized();
        }
    }

}
