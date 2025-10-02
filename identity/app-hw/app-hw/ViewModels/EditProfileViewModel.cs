using System.ComponentModel.DataAnnotations;

namespace app_hw.ViewModels
{
    public class EditProfileViewModel
    {
        [Required] public string FullName { get; set; }
        [Required, EmailAddress] public string Email { get; set; }
    }
}
