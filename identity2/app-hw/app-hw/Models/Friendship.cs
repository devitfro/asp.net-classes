namespace app_hw.Models
{
    public class Friendship
    {
        public int Id { get; set; }
        public string UserAId { get; set; }
        public ApplicationUser UserA { get; set; }

        public string UserBId { get; set; }
        public ApplicationUser UserB { get; set; }

        public DateTime Since { get; set; } = DateTime.UtcNow;
    }
}
