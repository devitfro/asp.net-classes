using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System.IO;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

app.MapGet("/", async context =>
{
    // Полный путь до index.html
    var filePath = Path.Combine(app.Environment.WebRootPath ?? "wwwroot", "index.html");
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.SendFileAsync(filePath);
});


app.MapPost("/upload", async (IFormFile file, IWebHostEnvironment env, HttpContext context) =>
{
    if (file == null || file.Length == 0)
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Файл не выбран");
    }

    var uploadPath = Path.Combine(env.WebRootPath ?? "wwwroot", "uploads");

    if (!Directory.Exists(uploadPath))
    {
        Directory.CreateDirectory(uploadPath);
    }

    var filePath = Path.Combine(uploadPath, file.FileName);

    using (var stream = new FileStream(filePath, FileMode.Create))
    {
        await file.CopyToAsync(stream);
    }

    var fileUrl = $"/uploads/{file.FileName}";

    return Results.Ok(new
    {
        message = "Файл успешно загружен",
        url = fileUrl
    });
});

app.Run();
