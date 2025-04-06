using AutoMapper;
using IamAlive.Data;
using IamAlive.DTOs.FriendshipDtos;
using IamAlive.Models;
using IamAlive.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace IamAlive.Services.Implementations
{
    public class FriendshipService : IFriendshipService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public FriendshipService( AppDbContext appDbContent, IMapper mapper) 
        {
            _appDbContext = appDbContent;
            _mapper = mapper;
        }

        public async Task<FriendshipDto> CreateFriendshipAsync(FriendshipCreateDto friendshipData)
        {
            var friendshipAlreadyExists = await _appDbContext.Friendships.AnyAsync(friendship =>
                (friendship.UserId == friendshipData.UserId && friendship.FriendId == friendshipData.FriendId) ||
                (friendship.UserId == friendshipData.FriendId && friendship.FriendId == friendshipData.UserId)
            );

            if (friendshipAlreadyExists)
            {
                throw new InvalidOperationException("Friendship already exists.");
            }

            var newFriendship = _mapper.Map<Friendship>(friendshipData);
            _appDbContext.Friendships.Add(newFriendship);
            await _appDbContext.SaveChangesAsync();

            return _mapper.Map<FriendshipDto>(newFriendship);
        }

        public async Task<IEnumerable<FriendshipDto>> GetFriendsForUserAsync(int userId)
        {
            var userFriendships = await _appDbContext.Friendships
                .Where(friendship => friendship.UserId == userId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<FriendshipDto>>(userFriendships);
        }

        public async Task<bool> DeleteFriendshipAsync(int userId, int friendId)
        {
            var friendship = await _appDbContext.Friendships
                .FirstOrDefaultAsync(f =>
                    (f.UserId == userId && f.FriendId == friendId) ||
                    (f.UserId == friendId && f.FriendId == userId));

            if (friendship == null)
                return false;

            _appDbContext.Friendships.Remove(friendship);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

    }
}
