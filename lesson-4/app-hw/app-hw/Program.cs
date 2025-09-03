using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IUsersList, UsersList>();

var app = builder.Build();

// Получаем сервис из DI
var usersList = app.Services.GetService<IUsersList>();

app.MapGet("/", () =>
{
    var html = $"""
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset="utf-8" />
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet">
        <title>User Management System</title>
    </head>
    <body>
        <div class="container mt-4">
            <h1 class="text-center">User Management System</h1>
            <div class="row mt-4">
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-body">
                            <a href="/users" class="btn btn-primary w-100 mb-2">View All Users</a>
                            <a href="/add-user" class="btn btn-success w-100 mb-2">Add New User</a>
                            <a href="/search-user" class="btn btn-info w-100">Search User</a>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-body">
                            <p>Total Users: {usersList.GetAllUsers().Count}</p>
   
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </body>
    </html>
    """;
    return Results.Content(html, "text/html");
});

app.MapGet("/users", () =>
{
    var users = usersList.GetAllUsers();
    var tableHtml = BuildTable(users);

    var html = $"""
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset="utf-8" />
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet">
        <title>All Users</title>
    </head>
    <body>
        <div class="container mt-4">
            <h1 class="text-center">All Users</h1>
            <a href="/" class="btn btn-secondary mb-3">← Back to Home</a>
            
            {(users.Count == 0 ?
                "<div class='alert alert-info'>No users found. <a href='/add-user'>Add first user</a></div>" :
                tableHtml)}
            
            <div class="mt-3">
                <a href="/add-user" class="btn btn-success">Add New User</a>
            </div>
        </div>
    </body>
    </html>
    """;
    return Results.Content(html, "text/html");
});

app.MapGet("/add-user", () =>
{
    var html = """
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset="utf-8" />
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet">
        <title>Add User</title>
    </head>
    <body>
        <div class="container mt-4">
            <h1 class="text-center">Add New User</h1>
            <a href="/users" class="btn btn-secondary mb-3">← Back to Users</a>
            
            <form method="post" action="/add-user" class="card p-4">
                <div class="mb-3">
                    <label for="name" class="form-label">Name</label>
                    <input type="text" class="form-control" id="name" name="name" required>
                </div>
                <div class="mb-3">
                    <label for="age" class="form-label">Age</label>
                    <input type="number" class="form-control" id="age" name="age" required>
                </div>
                <button type="submit" class="btn btn-primary">Add User</button>
            </form>
        </div>
    </body>
    </html>
    """;
    return Results.Content(html, "text/html");
});

app.MapPost("/add-user", async (HttpContext context) =>
{
    var form = await context.Request.ReadFormAsync();
    var name = form["name"].ToString();
    var ageStr = form["age"].ToString();

    if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(ageStr))
    {
        return Results.Content("Error: Name and age are required!", "text/html");
    }

    if (!int.TryParse(ageStr, out int age) || age < 1 || age > 120)
    {
        return Results.Content("Error: Age must be a number between 1 and 120!", "text/html");
    }

    if (usersList.GetUser(name) != null)
    {
        return Results.Content($"Error: User '{name}' already exists!", "text/html");
    }

    var user = new User { Name = name, Age = age };
    usersList.AddUser(user);

    var html = $"""
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset="utf-8" />
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet">
        <title>User Added</title>
    </head>
    <body>
        <div class="container mt-4">
            <div class="alert alert-success">
                <h4>User Added Successfully!</h4>
                <p>Name: {user.Name}</p>
                <p>Age: {user.Age}</p>
            </div>
            <a href="/users" class="btn btn-primary">View All Users</a>
            <a href="/add-user" class="btn btn-success">Add Another User</a>
        </div>
    </body>
    </html>
    """;
    return Results.Content(html, "text/html");
});

app.MapGet("/search-user", () =>
{
    var html = """
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset="utf-8" />
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet">
        <title>Search User</title>
    </head>
    <body>
        <div class="container mt-4">
            <h1 class="text-center">Search User</h1>
            <a href="/users" class="btn btn-secondary mb-3">← Back to Users</a>
            
            <form method="get" action="/user-details" class="card p-4">
                <div class="mb-3">
                    <label for="name" class="form-label">Enter User Name</label>
                    <input type="text" class="form-control" id="name" name="name" required>
                </div>
                <button type="submit" class="btn btn-primary">Search</button>
            </form>
        </div>
    </body>
    </html>
    """;
    return Results.Content(html, "text/html");
});

app.MapGet("/user-details", (string name) =>
{
    var user = usersList.GetUser(name);

    if (user == null)
    {
        return Results.Content($"""
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset="utf-8" />
            <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet">
            <title>User Not Found</title>
        </head>
        <body>
            <div class="container mt-4">
                <div class="alert alert-danger">
                    <h4>User Not Found</h4>
                    <p>User '{name}' does not exist.</p>
                </div>
                <a href="/search-user" class="btn btn-primary">Search Again</a>
                <a href="/users" class="btn btn-secondary">View All Users</a>
            </div>
        </body>
        </html>
        """, "text/html");
    }

    var html = $"""
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset="utf-8" />
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet">
        <title>User Details</title>
    </head>
    <body>
        <div class="container mt-4">
            <h1 class="text-center">User Details</h1>
            <a href="/users" class="btn btn-secondary mb-3">← Back to Users</a>
            
            <div class="card">
                <div class="card-header">
                    <h5>User Information</h5>
                </div>
                <div class="card-body">
                    <p><strong>Name:</strong> {user.Name}</p>
                    <p><strong>Age:</strong> {user.Age}</p>
                </div>
                <div class="card-footer">
                    <form method="post" action="/delete-user" style="display:inline;">
                        <input type="hidden" name="name" value="{user.Name}">
                        <button type="submit" class="btn btn-danger" 
                                onclick="return confirm('Are you sure you want to delete {user.Name}?')">
                            Delete User
                        </button>
                    </form>
                    <a href="/edit-user?name={user.Name}" class="btn btn-warning">✏️ Edit User</a>
                </div>
            </div>
        </div>
    </body>
    </html>
    """;
    return Results.Content(html, "text/html");
});

app.MapGet("/edit-user", (string name) =>
{
    var user = usersList.GetUser(name);

    if (user == null)
    {
        return Results.Content("User not found!", "text/html");
    }

    var html = $"""
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset="utf-8" />
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet">
        <title>Edit User</title>
    </head>
    <body>
        <div class="container mt-4">
            <h1 class="text-center">Edit User</h1>
            <a href="/user-details?name={user.Name}" class="btn btn-secondary mb-3">← Back</a>
            
            <form method="post" action="/edit-user" class="card p-4">
                <input type="hidden" name="oldName" value="{user.Name}">
                <div class="mb-3">
                    <label for="name" class="form-label">Name</label>
                    <input type="text" class="form-control" id="name" name="name" value="{user.Name}" required>
                </div>
                <div class="mb-3">
                    <label for="age" class="form-label">Age</label>
                    <input type="number" class="form-control" id="age" name="age" value="{user.Age}" required 
                           min="1" max="120">
                </div>
                <button type="submit" class="btn btn-primary">Update User</button>
            </form>
        </div>
    </body>
    </html>
    """;
    return Results.Content(html, "text/html");
});

app.MapPost("/edit-user", async (HttpContext context) =>
{
    var form = await context.Request.ReadFormAsync();
    var oldName = form["oldName"].ToString();
    var newName = form["name"].ToString();
    var ageStr = form["age"].ToString();

    if (!int.TryParse(ageStr, out int age) || age < 1 || age > 120)
    {
        return Results.Content("Error: Invalid age!", "text/html");
    }

    var existingUser = usersList.GetUser(newName);
    if (existingUser != null && newName != oldName)
    {
        return Results.Content($"Error: User '{newName}' already exists!", "text/html");
    }

    var updatedUser = new User { Name = newName, Age = age };
    usersList.UpdateUser(oldName, updatedUser);

    var html = $"""
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset="utf-8" />
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet">
        <title>User Updated</title>
    </head>
    <body>
        <div class="container mt-4">
            <div class="alert alert-success">
                <h4>User Updated Successfully!</h4>
                <p>Name: {updatedUser.Name}</p>
                <p>Age: {updatedUser.Age}</p>
            </div>
            <a href="/user-details?name={updatedUser.Name}" class="btn btn-primary">View User</a>
            <a href="/users" class="btn btn-secondary">View All Users</a>
        </div>
    </body>
    </html>
    """;
    return Results.Content(html, "text/html");
});

app.MapPost("/delete-user", async (HttpContext context) =>
{
    var form = await context.Request.ReadFormAsync();
    var name = form["name"].ToString();

    var user = usersList.GetUser(name);
    if (user == null)
    {
        return Results.Content("Error: User not found!", "text/html");
    }

    usersList.DeleteUser(name);

    var html = $"""
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset="utf-8" />
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet">
        <title>User Deleted</title>
    </head>
    <body>
        <div class="container mt-4">
            <div class="alert alert-info">
                <h4>User Deleted Successfully!</h4>
                <p>User '{name}' has been removed from the system.</p>
            </div>
            <a href="/users" class="btn btn-primary">View All Users</a>
            <a href="/add-user" class="btn btn-success">Add New User</a>
        </div>
    </body>
    </html>
    """;
    return Results.Content(html, "text/html");
});

app.Run();

string BuildTable<T>(IEnumerable<T> collection)
{
    if (collection == null || !collection.Any())
        return "<div class='alert alert-info'>No data available</div>";

    var sb = new StringBuilder();
    var properties = typeof(T).GetProperties();

    sb.Append("<table class='table table-striped table-bordered'>");
    sb.Append("<thead class='table-dark'><tr>");
    foreach (var prop in properties)
    {
        sb.Append($"<th>{prop.Name}</th>");
    }
    sb.Append("</tr></thead><tbody>");

    foreach (var item in collection)
    {
        sb.Append("<tr>");
        foreach (var prop in properties)
        {
            var value = prop.GetValue(item)?.ToString() ?? "N/A";
            sb.Append($"<td>{value}</td>");
        }
        sb.Append("</tr>");
    }
    sb.Append("</tbody></table>");

    return sb.ToString();
}

// Интерфейсы и классы
public interface IUser
{
    string Name { get; set; }
    int Age { get; set; }
    void ShowInfo();
}

public interface IUsersList
{
    void AddUser(User user);
    void DeleteUser(string name);
    User GetUser(string name);
    void UpdateUser(string name, User updatedUser);
    List<User> GetAllUsers();
}

public class User : IUser
{
    public string Name { get; set; }
    public int Age { get; set; }

    public void ShowInfo()
    {
        Console.WriteLine($"Name: {Name}, Age: {Age}");
    }
}

public class UsersList : IUsersList
{
    private List<User> users = new List<User>();

    public void AddUser(User user)
    {
        users.Add(user);
        Console.WriteLine($"Added user: {user.Name}");
    }

    public void DeleteUser(string name)
    {
        var user = GetUser(name);
        if (user != null)
        {
            users.Remove(user);
            Console.WriteLine($"Deleted user: {name}");
        }
    }

    public User GetUser(string name)
    {
        return users.FirstOrDefault(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public void UpdateUser(string name, User updatedUser)
    {
        var user = GetUser(name);
        if (user != null)
        {
            user.Name = updatedUser.Name;
            user.Age = updatedUser.Age;
            Console.WriteLine($"Updated user: {name} to {updatedUser.Name}");
        }
    }

    public List<User> GetAllUsers()
    {
        return users;
    }
}