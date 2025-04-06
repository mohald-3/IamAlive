using IamAlive.DTOs.UserDtos;

namespace IamAlive.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> RegisterUserAsync(UserCreateDto userRegistrationData);
        Task<LoginResponseDto?> LoginUserAsync(LoginDto loginData);
    }
}
