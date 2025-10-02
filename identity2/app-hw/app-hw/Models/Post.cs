using System.ComponentModel.DataAnnotations;

namespace app_hw.Models
{
    public enum PostVisibility
    {
        Public,     // всі бачать
        Private,    // тільки автор
        FriendsOnly // тільки друзі автора
    }

    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }

        public PostVisibility Visibility { get; set; } = PostVisibility.Public;

        public bool IsHidden { get; set; } = false; // для модератора
    }
}
