using app_class_1.Models;
using Microsoft.AspNetCore.Mvc;

namespace app_class_1.Components
{
    public class PersonViewComponent : ViewComponent
    {
        public string Invoke(User user)
        {
            return $"Name - {user.Name}; age = {user.Age}";
        }
    }
}
