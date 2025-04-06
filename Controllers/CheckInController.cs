using IamAlive.DTOs.CheckInDtos;
using IamAlive.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IamAlive.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckInController : ControllerBase
    {
        private readonly ICheckInService _checkInService;

        public CheckInController(ICheckInService checkInService) 
        {
            _checkInService = checkInService;
        }

        // POST: api/checkin
        [HttpPost]
        public async Task<IActionResult> CreateCheckIn([FromBody] CheckInCreateDto checkInData)
        {
            var createdCheckIn = await _checkInService.CreateCheckInAsync(checkInData);
            return Ok(createdCheckIn);
        }

        // GET: api/checkin/user/5
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserCheckIns(int userId)
        {
            var checkIns = await _checkInService.GetCheckInsByUserIdAsync(userId);
            return Ok(checkIns);
        }
    }
}
