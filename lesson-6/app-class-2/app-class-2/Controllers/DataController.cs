using Microsoft.AspNetCore.Mvc;
using app_class_2.Models;

namespace app_class_2.Controllers
{
    public class DataController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string GetDataString(string text)
        {
            return $"Info: {text}";
        }

        public string GetPet(Pet pet)
        {
            return pet.ToString();
        }

        public string GetMultipleData(string name, int age, bool hasPet, Pet pet)
        {
            return $"Name: {name}, Age: {age}, Has pet: {hasPet}, Pet: {pet}";
        }
    }
}
