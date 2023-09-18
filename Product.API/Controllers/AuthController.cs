using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.BLL.Abstract;
using Product.Entity.Dtos.UserDtos;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginDto userLoginDto)
        {
            var result = _authService.Login(userLoginDto);
            return Ok(result);
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterDto userRegisterDto)
        {
            var userExists = _authService.UserExists(userRegisterDto.Email);
            if (!userExists.Success)
            {
                return Ok(userExists);
            }
            var result = _authService.Register(userRegisterDto);
            return Ok(result);
        }
    }
}
