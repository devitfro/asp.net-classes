using app_hw.Models;
using Microsoft.AspNetCore.Identity;

namespace app_hw.Data
{            
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public ICollection<Note> Notes { get; set; }
    }
    
}
