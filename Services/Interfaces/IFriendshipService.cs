using IamAlive.DTOs.FriendshipDtos;

namespace IamAlive.Services.Interfaces
{
    public interface IFriendshipService
    {
        Task<FriendshipDto> CreateFriendshipAsync(FriendshipCreateDto friendshipData);
        Task<IEnumerable<FriendshipDto>> GetFriendsForUserAsync(int userId);
        Task<bool> DeleteFriendshipAsync(int userId, int friendId);

    }
}
