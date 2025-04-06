using IamAlive.DTOs.UserDtos;

public class LoginResponseDto
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Expiry { get; set; }

    public UserDto User { get; set; }
}
