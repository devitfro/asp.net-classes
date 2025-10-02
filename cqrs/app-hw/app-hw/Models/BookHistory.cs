namespace app_hw.Models
{
    public class BookHistory
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string ChangedBy { get; set; } = "system";
        public DateTime ChangedAt { get; set; }
        public string Summary { get; set; } = null!;
    }
}
