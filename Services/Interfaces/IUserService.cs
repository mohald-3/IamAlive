using IamAlive.DTOs.UserDtos;

namespace IamAlive.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetFilteredUsersAsync(string? search, string? sortBy, string? sortOrder, int page = 1, int pageSize = 10);
        Task<UserDto?> GetUserByIdAsync(int userId);
        Task<UserDto> RegisterUserAsync(UserCreateDto userRegistrationData);
        Task<LoginResponseDto?> LoginUserAsync(LoginDto loginData);
        Task<UserDto?> UpdateUserAsync(int userId, UserUpdateDto updatedData);
        Task<bool> SoftDeleteUserAsync(int userId);

    }
}
