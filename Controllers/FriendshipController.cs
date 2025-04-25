using ApplicationLayer.DTOs.FriendshipDtos;
using ApplicationLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FriendshipController : ControllerBase
    {
        private readonly IFriendshipService _friendshipService;

        public FriendshipController(IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
        }

        // Post: api/friendship
        [HttpPost]
        public async Task <IActionResult> CreateFriendship([FromBody] FriendshipCreateDto friendshipData)
        {
            try
            {
                var createdFriendship = await _friendshipService.CreateFriendshipAsync(friendshipData);
                return Ok(createdFriendship);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/friendship/user/5
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserFriendships(int userId)
        {
            var friendships = await _friendshipService.GetFriendsForUserAsync(userId);
            return Ok(friendships);
        }

        // DELETE: api/friendship/{userId}/{friendId}
        [HttpDelete("{userId}/{friendId}")]
        public async Task<IActionResult> DeleteFriendship(int userId, int friendId)
        {
            var success = await _friendshipService.DeleteFriendshipAsync(userId, friendId);
            if (!success)
                return NotFound("Friendship not found.");

            return NoContent();
        }

    }
}
