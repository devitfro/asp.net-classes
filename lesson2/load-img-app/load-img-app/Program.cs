using Microsoft.Extensions.FileProviders;
using System.Text;

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "img")),
    RequestPath = "/img"
});

app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;

    response.ContentType = "text/html; charset=utf-8";

    if (request.Path == "/")
    {
        string imgFolder = Path.Combine(Directory.GetCurrentDirectory(), "img");
        var files = Directory.GetFiles(imgFolder);

        StringBuilder body = new StringBuilder();
        body.Append("<h2>Список изображений:</h2>");
        body.Append("<ul>");
        foreach (var file in files)
        {
            string fileName = Path.GetFileName(file);
            body.Append($"<li><a href=\"/img/{fileName}\" target=\"_blank\">{fileName}</a></li>");
        }
        body.Append("</ul>");

     
        await response.WriteAsync(body.ToString());
    }
});

app.Run();