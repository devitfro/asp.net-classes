namespace app_hw.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Author { get; set; }
        public int? Year { get; set; }

        // Флаг блокировки при редактировании
        public bool IsLocked { get; set; } = false;
    }
}
