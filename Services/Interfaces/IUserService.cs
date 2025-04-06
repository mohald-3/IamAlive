using IamAlive.DTOs.UserDtos;

namespace IamAlive.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(int userId);
        Task<UserDto> RegisterUserAsync(UserCreateDto userRegistrationData);
        Task<LoginResponseDto?> LoginUserAsync(LoginDto loginData);
        Task<UserDto?> UpdateUserAsync(int userId, UserUpdateDto updatedData);
        Task<bool> SoftDeleteUserAsync(int userId);


    }
}
