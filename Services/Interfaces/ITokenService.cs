using IamAlive.Models;

namespace IamAlive.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
