namespace IamAlive.DTOs.CheckInDtos
{
    public class CheckInDto // this might needs changing depending on what i want to do, the main functionality should be returning last time checked in and if the use have checked in
    {
        public int CheckInId { get; set; }

        public DateTime Timestamp { get; set; }

        public int UserId { get; set; }
    }
}
