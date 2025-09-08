using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using app_class_1.Models;
using app_class_1.Util;

namespace app_class_1.Controllers
{
    //[NonController]
    public class CompanyController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public CompanyController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index()
        {
            // return new HtmlResult("<h2>Hello from HtmlResult</h2>");
            // return new ObjectResult( "name = name");
            // return Redirect("~/Company/GetMessage?name=Alex");
            // return RedirectToAction("GetMessage", new { message = "Hello", age = 25 });
            return View();
        }
        
        public IActionResult GetFile()
        {
            
            string file_path = _env.WebRootPath + "/Files/img.png";
            
            // 1 способ передатьт файл
            // return PhysicalFile(file_path, "text/plain");

            // 2 способ передать файл
            // byte[] file_bytes = System.IO.File.ReadAllBytes(file_path);
            // return File(file_bytes, "text/plain");

            // 3 способ не загружать в массив байт, а передать потоком в FileStream
            // FileStream fs = new FileStream(file_path, FileMode.Open);
            // return File(fs, "image/png");

            // 4 способ идеальный если файл в wwwroot
            return File(file_path, "text/plain");      
        }
    }
}
