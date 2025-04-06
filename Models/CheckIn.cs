using System.ComponentModel.DataAnnotations;

namespace IamAlive.Models
{
    public class CheckIn
    {
        public int CheckInId { get; set; }
        public DateTime Timestamp { get; set; }
        // Foreign Key
        public int UserId { get; set; }

        // Navigation property
        public virtual User User { get; set; }
    }
}
