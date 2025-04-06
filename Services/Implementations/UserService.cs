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
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UserService(AppDbContext context, IMapper mapper, ITokenService tokenService)
        {
            _appDbContext = context;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var usersFromDatabase = await _appDbContext.Users
                .Where(user => !user.IsDeleted)
                .OrderBy(user => user.FirstName)
                .ToListAsync();

            var mappedUserList = _mapper.Map<IEnumerable<UserDto>>(usersFromDatabase);
            return mappedUserList;
        }

        public async Task<IEnumerable<UserDto>> GetFilteredUsersAsync(string? search, string? sortBy, string? sortOrder, int page = 1, int pageSize = 10)
        {
            var query = _appDbContext.Users.AsQueryable();

            // Filter by name, email, or phone
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                query = query.Where(u =>
                    u.FirstName.ToLower().Contains(search) ||
                    u.LastName.ToLower().Contains(search) ||
                    u.Email.ToLower().Contains(search) ||
                    u.Phone.Contains(search));
            }

            // Sorting
            switch (sortBy?.ToLower())
            {
                case "name":
                    query = sortOrder == "asc"
                        ? query.OrderBy(user => user.FirstName).ThenBy(user => user.LastName)
                        : query.OrderByDescending(user => user.FirstName).ThenByDescending(user => user.LastName);
                    break;
                case "email":
                    query = sortOrder == "asc"
                        ? query.OrderBy(user => user.Email)
                        : query.OrderByDescending(user => user.Email);
                    break;
                case "created":
                    query = sortOrder == "asc"
                        ? query.OrderBy(user => user.AccountCreationTime)
                        : query.OrderByDescending(user => user.AccountCreationTime);
                    break;
                default:
                    query = query.OrderByDescending(user => user.AccountCreationTime); // Default sort
                    break;
            }

            // Pagination 
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var users = await query.ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }


        public async Task<UserDto?> GetUserByIdAsync(int userId)
        {
            var user = await _appDbContext.Users
                .FirstOrDefaultAsync(user => user.UserId == userId && !user.IsDeleted);

            if (user == null)
                return null;

            return _mapper.Map<UserDto>(user);
        }


        public async Task<UserDto> RegisterUserAsync(UserCreateDto userRegistrationData)
        {
            var newUser = _mapper.Map<User>(userRegistrationData);
            newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userRegistrationData.Password);
            
            _appDbContext.Users.Add(newUser);
            await _appDbContext.SaveChangesAsync();

            return _mapper.Map<UserDto>(newUser);
        }

        public async Task<LoginResponseDto?> LoginUserAsync(LoginDto loginData)
        {
            var userFromDatabase = await _appDbContext.Users
                .FirstOrDefaultAsync(user => user.Email == loginData.Email && !user.IsDeleted);

            if (userFromDatabase == null || !BCrypt.Net.BCrypt.Verify(loginData.Password, userFromDatabase.PasswordHash))
                return null;

            var jwtToken = _tokenService.CreateToken(userFromDatabase);

            // Generate Refresh Token + expiry
            userFromDatabase.RefreshToken = GenerateRefreshToken();
            userFromDatabase.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            _appDbContext.Users.Update(userFromDatabase);
            await _appDbContext.SaveChangesAsync();

            var loginResponse = new LoginResponseDto
            {
                Token = jwtToken,
                Expiry = DateTime.UtcNow.AddMinutes(60),
                User = _mapper.Map<UserDto>(userFromDatabase)
            };

            return loginResponse;
        }

        public async Task<UserDto?> UpdateUserAsync(int userId, UserUpdateDto updatedData)
        {
            var user = await _appDbContext.Users.FindAsync(userId);
            if (user == null || user.IsDeleted)
                return null;

            // Apply updates only if values are provided
            if (!string.IsNullOrWhiteSpace(updatedData.FirstName))
                user.FirstName = updatedData.FirstName;

            if (!string.IsNullOrWhiteSpace(updatedData.LastName))
                user.LastName = updatedData.LastName;

            if (!string.IsNullOrWhiteSpace(updatedData.Phone))
                user.Phone = updatedData.Phone;

            if (!string.IsNullOrWhiteSpace(updatedData.EmergencyContactName))
                user.EmergencyContactName = updatedData.EmergencyContactName;

            if (!string.IsNullOrWhiteSpace(updatedData.EmergencyContactPhone))
                user.EmergencyContactPhone = updatedData.EmergencyContactPhone;

            user.AccountModificationTime = DateTime.UtcNow;

            await _appDbContext.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> SoftDeleteUserAsync(int userId)
        {
            var user = await _appDbContext.Users.FindAsync(userId);
            if (user == null || user.IsDeleted)
                return false;

            user.IsDeleted = true;
            user.AccountModificationTime = DateTime.UtcNow;

            await _appDbContext.SaveChangesAsync();
            return true;
        }


        //Helper method for token generation
        private string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            System.Security.Cryptography.RandomNumberGenerator.Fill(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}
