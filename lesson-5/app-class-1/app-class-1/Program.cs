using app_class_1.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Map(
    "{controller=Home}/{action=Index}/{id?}",
    (string controller, string action, string? id) =>
        $"Controller: {controller} \nAction: {action} \nId: {id}"
);

//app.Map("/", IndexHandex);

//app.Map("about", () => "About page");

//app.Map("contact", () => "Contact page");

//app.Map("console", () => Console.WriteLine("Console"));

//app.Map("/userData/{id}", async(string id, HttpContext context) => await context.Response.WriteAsJsonAsync(new { Id = id, Name = "Alex", Age = 20 }));

//app.Map("/users/{id}/{name}", (string id, string name) => $"Name = {name}, id = {id}");

app.Run();

//async Task IndexHandex(HttpContext context)
//{
//    await context.Response.WriteAsync("Hello");
//}