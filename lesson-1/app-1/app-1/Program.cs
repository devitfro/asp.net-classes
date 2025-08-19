var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// task 1
app.Run(async context =>
{
    var response = context.Response;
    response.ContentType = "text/html; charset=utf-8";

    var request = context.Request;

    var path = request.Path;

    var res = "";


    if (!string.IsNullOrEmpty(request.QueryString.Value))
    {
        res = request.QueryString.ToString().TrimStart('?');
    }
    else if (path.StartsWithSegments("/api/length", out var remaining))
    {
        res = remaining.Value.TrimStart('/');
    }
    var len = res.Length;

    await response.WriteAsync("<h1>Length query</h1>" + $"{len}");

});

app.Run();

// task 2
var users = new List<Person>();

app.UseStaticFiles();

app.Run(async context =>
{
    var response = context.Response;
    var request = context.Request;
    response.ContentType = "text/html; charset=utf-8";

    if (request.Path == "/")
    {

        response.Redirect("/html/form.html");
        return;
    }

    if (request.Path == "/formuser")
    {
        var form = await request.ReadFormAsync();

        users.Add(new Person
        {
            Name = form["name"],
            Email = form["email"],
            Phone = form["phone"]
        });
        response.Redirect("/html/greeting.html");
        return;
    }
});
app.Run();

public class Person
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}

