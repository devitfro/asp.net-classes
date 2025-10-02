using Microsoft.Extensions.Hosting;

namespace app_hw.Models
{
    public class ApplicationUser
    {
        public string DisplayName { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Friendship> Friendships { get; set; } 
    }
}
