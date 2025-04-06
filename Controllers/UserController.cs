using AutoMapper;
using IamAlive.DTOs.UserDtos;
using IamAlive.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IamAlive.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // POST: api/User/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateDto userRegistrationData)
        {
            var result = await _userService.RegisterUserAsync(userRegistrationData);
            return Ok(result);
        }

        // POST: api/User/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginData)
        {
            var result = await _userService.LoginUserAsync(loginData);
            if (result == null)
                return Unauthorized("Invalid credentials.");

            return Ok(result); // result should be a LoginResponseDto
        }
    }
}
