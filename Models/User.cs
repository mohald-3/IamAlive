namespace IamAlive.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public DateTime AccountCreationTime { get; set; } = DateTime.UtcNow;
        public DateTime? AccountModificationTime {  get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;

        // For JWT/Refresh Tokens

        public string? RefreshToken { get; set; } = string.Empty;
        public DateTime? RefreshTokenExpiry { get; set; } = DateTime.UtcNow.AddDays(7);

        // --- Navigation Properties ---
        public virtual ICollection<CheckIn> CheckIns { get; set; }
        public virtual ICollection<Friendship> Friendships { get; set; }

        //// 3. GroupMemberships (many-to-many with SocialGroup)
        //// The bridging table is GroupMembership => user_id + group_id.
        //public virtual ICollection<GroupMembership> GroupMemberships { get; set; }

        //// 4. SocialGroups Created (1-to-many)
        //// A user can create many SocialGroups, referencing 'created_by'.
        //public virtual ICollection<SocialGroup> CreatedSocialGroups { get; set; }

    }
}
