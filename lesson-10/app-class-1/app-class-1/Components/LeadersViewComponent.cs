using app_class_1.Models;
using Microsoft.AspNetCore.Mvc;

namespace app_class_1.Components
{
    public class LeadersViewComponent : ViewComponent
    {
        Dictionary<string, int> leaders;

        public LeadersViewComponent()
        {
            leaders = new Dictionary<string, int>
            {
                { "Tom", 100 },
                { "Bob", 100 },
                { "Sam", 100 },
                { "Tom2", 100 },
                { "Tom3", 100 },
                { "Tom4", 100 },
                { "Tom5", 100 },
                { "Tom6", 990 },
                { "Tom7", 950 },
                { "Tom8", 90 }
            };
        }

        public IViewComponentResult Invoke()
        {
            int number = leaders.Count;

            if (Request.Query.ContainsKey("number"))
            {
                Int32.TryParse(Request.Query["number"].ToString(), out number);
            }

            ViewBag.Users = leaders.Take(number);
            ViewData["Header"] = $"Top users: {number}";
            return View();        
        }

    }
}
