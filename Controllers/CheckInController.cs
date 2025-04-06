using IamAlive.DTOs.CheckInDtos;
using IamAlive.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IamAlive.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CheckInController : ControllerBase
    {
        private readonly ICheckInService _checkInService;

        public CheckInController(ICheckInService checkInService) 
        {
            _checkInService = checkInService;
        }

        // GET: api/checkin/user/5
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserCheckIns(int userId)
        {
            var checkIns = await _checkInService.GetCheckInsByUserIdAsync(userId);
            return Ok(checkIns);
        }

        // GET: api/checkin/{id}
        [HttpGet("{checkInId}")]
        public async Task<IActionResult> GetCheckInById(int checkInId)
        {
            var checkIn = await _checkInService.GetCheckInByIdAsync(checkInId);
            if (checkIn == null)
                return NotFound();

            return Ok(checkIn);
        }

        // POST: api/checkin
        [HttpPost]
        public async Task<IActionResult> CreateCheckIn([FromBody] CheckInCreateDto checkInData)
        {
            var createdCheckIn = await _checkInService.CreateCheckInAsync(checkInData);
            return Ok(createdCheckIn);
        }

        // DELETE: api/checkin/{id}
        [HttpDelete("{checkInId}")]
        public async Task<IActionResult> DeleteCheckIn(int checkInId)
        {
            var success = await _checkInService.DeleteCheckInAsync(checkInId);
            if (!success)
                return NotFound();

            return NoContent(); // 204 No Content
        }
    }
}
