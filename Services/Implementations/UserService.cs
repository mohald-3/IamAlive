using AutoMapper;
using IamAlive.Data;
using IamAlive.DTOs.UserDtos;
using IamAlive.Models;
using IamAlive.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IamAlive.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> RegisterUserAsync(UserCreateDto userRegistrationData)
        {
            var newUser = _mapper.Map<User>(userRegistrationData);
            newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userRegistrationData.Password);
            
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(newUser);
        }

        public async Task<LoginResponseDto?> LoginUserAsync(LoginDto loginData)
        {
            var userFromDatabase = await _context.Users.FirstOrDefaultAsync(user => user.Email == loginData.Email);
            if (userFromDatabase == null || !BCrypt.Net.BCrypt.Verify(loginData.Password, userFromDatabase.PasswordHash))
                return null;

            // For now, return a simple fake response — will update with JWT later
            var loginResponse = new LoginResponseDto
            {
                Token = "fake-jwt-token", // this will be replaced with a real JWT later
                Expiry = DateTime.UtcNow.AddHours(1),
                User = _mapper.Map<UserDto>(userFromDatabase)
            };
            return loginResponse;
        }
    }
}
