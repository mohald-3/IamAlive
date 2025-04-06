using AutoMapper;
using IamAlive.Data;
using IamAlive.DTOs.CheckInDtos;
using IamAlive.Models;
using IamAlive.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IamAlive.Services.Implementations
{
    public class CheckInService : ICheckInService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public CheckInService (AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<CheckInDto> CreateCheckInAsync(CheckInCreateDto checkInData)
        {
            // Map the DTO to a CheckIn entity
            var newCheckIn = _mapper.Map<CheckIn>(checkInData);
            newCheckIn.Timestamp = DateTime.UtcNow;

            // Add and save to the database
            _appDbContext.CheckIns.Add(newCheckIn);
            await _appDbContext.SaveChangesAsync();

            // Return mapped DTO
            var createdCheckIn = _mapper.Map<CheckInDto>(newCheckIn);
            return createdCheckIn;
        }

        public async Task<CheckInDto?> GetCheckInByIdAsync(int checkInId)
        {
            var checkIn = await _appDbContext.CheckIns.FindAsync(checkInId);
            return checkIn == null ? null : _mapper.Map<CheckInDto>(checkIn);
        }


        public async Task<IEnumerable<CheckInDto>> GetCheckInsByUserIdAsync(int userId)
        {
            // Retrieve all check-ins for the specified user, ordered by most recent
            var userCheckInsFromDatabase = await _appDbContext.CheckIns
                .Where(checkIn => checkIn.UserId == userId)
                .OrderByDescending(checkIn => checkIn.Timestamp)
                .ToListAsync();

            // Map the retrieved check-ins to a list of CheckInDto objects
            var mappedCheckInList = _mapper.Map<IEnumerable<CheckInDto>>(userCheckInsFromDatabase);

            return mappedCheckInList;
        }

        public async Task<bool> DeleteCheckInAsync(int checkInId)
        {
            var checkIn = await _appDbContext.CheckIns.FindAsync(checkInId);
            if (checkIn == null)
                return false;

            _appDbContext.CheckIns.Remove(checkIn);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

    }
}
