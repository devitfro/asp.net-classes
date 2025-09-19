using asp_class_2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asp_class_2.ViewModels
{
    public class UserListViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public SelectList Companies { get; set; }
        public string Name { get; set; }

    }
}
