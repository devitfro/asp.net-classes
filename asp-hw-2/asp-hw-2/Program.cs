
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async context =>
{
    var response = context.Response;
    var request = context.Request;

    response.ContentType = "text/html; charset=utf-8";

    if (request.Path == "/")
    {
        await response.SendFileAsync("html/mainpage.html");
    }

    if (request.Path == "/searchcity" && request.Method == "POST")
    {
        var apiKey = "4964b1b98a324c989a3132543251908";

        using var reader = new StreamReader(request.Body);
        var body = await reader.ReadToEndAsync();

        var json = JsonSerializer.Deserialize<Dictionary<string, string>>(body);
        var city = json?["city"];

        if (string.IsNullOrEmpty(city))
        {
            response.StatusCode = 400;
            await context.Response.WriteAsync("City not provided.");
            return;
        }

        using var httpClient = new HttpClient();
        var url = $"https://api.weatherapi.com/v1/current.json?key={apiKey}&q={city}&aqi=no";

        try
        {
            var resp = await httpClient.GetStringAsync(url);
            await response.WriteAsync(resp);
        }
        catch
        {
            response.StatusCode = 500;
            await response.WriteAsync("Error fetching weather.");
        }
    }
});

app.Run();
