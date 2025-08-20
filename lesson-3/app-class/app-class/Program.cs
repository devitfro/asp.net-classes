// task 1
// —оздать веб-приложение на Asp.Net Core, которое будет использовать middleware дл€ обработки HTTP запросов. ¬еб-приложение должно использовать middleware дл€ логировани€ запросов, добавлени€ заголовков ответов и обработки ошибок (дл€ каждого действи€ Ц свой метод Use). 

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (context, next) =>
{
    try
    {
        await next.Invoke();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("An error occurred.");
    }
});

app.Use(async (context, next) =>
{
    var request = context.Request;

    Console.WriteLine($"Request: {request.QueryString} {request.Method} {request.Path}");

    await next.Invoke();
});

app.Use(async (context, next) =>
{
    context.Response.Headers["Some Header"] = "Custom header";

    await next.Invoke();
});

app.Run(async context =>
{
    await context.Response.WriteAsync("Hello, World!");
});

app.Run();

// task 2
// —оздать сайт состо€щией из 5 страниц. ќтветом на запрос пользовател€, возвращайте страницей html с определенным содержимым. ѕри переходе на несуществующую страницу, выведите ошибку 404. »спользуйте UseWhen или MapWhen, дл€ создание отдельной ветки middleware.

using System.IO;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

void OpenPage(string path, string fileName)
{
    app.MapWhen(context => context.Request.Path == path, appBuilder =>
    {
        appBuilder.Run(async context =>
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.SendFileAsync(fileName);
        });
    });
}

OpenPage("/about", "html/about.html");
OpenPage("/contact", "html/contact.html");
OpenPage("/something", "html/something.html");
OpenPage("/favorite", "html/favorite.html");
OpenPage("/greeting", "html/greeting.html");

app.Run(async context =>
{
    context.Response.StatusCode = 404;
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.WriteAsync("<h1>404 - Page not found!</h1>");
});
app.Run();




