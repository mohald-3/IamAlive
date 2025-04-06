using IamAlive.DTOs.CheckInDtos;

namespace IamAlive.Services.Interfaces
{
    public interface ICheckInService
    {
        Task<CheckInDto> CreateCheckInAsync(CheckInCreateDto CheckInData);
        Task<IEnumerable<CheckInDto>> GetCheckInsByUserIdAsync(int userId);
        Task<bool> DeleteCheckInAsync(int checkInId);
        Task<CheckInDto?> GetCheckInByIdAsync(int checkInId);

    }
}
