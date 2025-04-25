using AutoMapper;
using ApplicationLayer.DTOs.UserDtos;
using ApplicationLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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

        // GET: api/user
        [HttpGet]
        public async Task<IActionResult> GetFilteredUsers(
            [FromQuery] string? search,
            [FromQuery] string? sort = "created",
            [FromQuery] string? order = "desc",
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var users = await _userService.GetFilteredUsersAsync(search, sort, order, page, pageSize);
            return Ok(users);
        }

        // GET: api/user/{id}
        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
                return NotFound();

            return Ok(user);
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

        // PATCH: api/user/{id}
        [Authorize]
        [HttpPatch("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserUpdateDto updatedData)
        {
            var updatedUser = await _userService.UpdateUserAsync(userId, updatedData);
            if (updatedUser == null)
                return NotFound();

            return Ok(updatedUser);
        }

        // DELETE: api/user/{id}
        [Authorize]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> SoftDeleteUser(int userId)
        {
            var success = await _userService.SoftDeleteUserAsync(userId);
            if (!success)
                return NotFound();

            return NoContent(); // 204 — successful but nothing returned
        }

    }
}
