namespace app_hw.Models
{
    public enum FriendRequestStatus
    {
        Pending,
        Accepted,
        Rejected
    }

    public class FriendRequest
    {
        public int Id { get; set; }

        public string FromUserId { get; set; }
        public ApplicationUser FromUser { get; set; }

        public string ToUserId { get; set; }
        public ApplicationUser ToUser { get; set; }

        public FriendRequestStatus Status { get; set; } = FriendRequestStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
