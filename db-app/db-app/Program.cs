using Microsoft.Data.SqlClient;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var configurationService = app.Services.GetService<IConfiguration>();
string connectionString = configurationService["ConnectionStrings:DefaultConnection"];

static string BuildHtmlTable<T>(IEnumerable<T> collection)
{
    StringBuilder tableHtml = new StringBuilder();
    tableHtml.Append("<table>");

    PropertyInfo[] properties = typeof(T).GetProperties();

    tableHtml.Append("<tr>");

    foreach (PropertyInfo property in properties)
    {
        tableHtml.Append($"<th>{property.Name}</th>");
    }

    tableHtml.Append("</tr>");

    foreach (T item in collection)       
    {
        tableHtml.Append("<tr>");
        foreach (PropertyInfo property in properties)
        {
            object value = property.GetValue(item);
            tableHtml.Append($"<td>{value}</td>");
        }

        tableHtml.Append("</tr>");
    }

    tableHtml.Append("</table>");

    return tableHtml.ToString();
}

static async Task CreateUser(string username, string age, string connectionString)
{
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        await connection.OpenAsync();

        string query = "INSERT INTO Users (Name, Age) VALUES (@Name, @Age)";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Name", username);
            command.Parameters.AddWithValue("@Age", age);

            await command.ExecuteNonQueryAsync();
        }
    }
}

static async Task DeleteUser(string username, string connectionString)
{
    using (SqlConnection connection = new SqlConnection(connectionString)) {
        await connection.OpenAsync();
        string query = "DELETE FROM Users WHERE Name = @Name";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Name", username);
            await command.ExecuteNonQueryAsync();
        }
    }
}

static async Task UpdateUser(string username, string newName, string newAge, string connectionString)
{
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        await connection.OpenAsync();
        string query = "UPDATE Users SET Name=@newName, Age=@newAge WHERE Name = @Name";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@newName", newName);
            command.Parameters.AddWithValue("@newAge", newAge);
            command.Parameters.AddWithValue("@Name", username);
            await command.ExecuteNonQueryAsync();
        }
    }
}

static async Task<List<User>> FindUser(string name, string connectionString)
{
    List<User> users = new List<User>();

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        await connection.OpenAsync();
        string query = "SELECT Id, Name, Age FROM Users WHERE Name LIKE @Name";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Name", $"%{name}%");

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    users.Add(new User(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetInt32(2)
                    ));
                }
            }
        }
    }

    return users;
}

static async Task<List<User>> SortUsers(string connectionString)
{
    List<User> users = new List<User>();

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        await connection.OpenAsync();

        string query = "SELECT Id, Name, Age FROM Users ORDER BY Age";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    users.Add(new User(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetInt32(2)
                    ));
                }
            }
        }
    }

    return users;
}

static string GenerateHtmlPage(string body, string header)
{
    string html = $"""
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset="utf-8" />
            <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet" 
            integrity="sha384-KK94CHFLLe+nY2dmCWGMq91rCGa5gtU4mk92HdvYe+M/SXH301p5ILy+dN9+nJOZ" crossorigin="anonymous">
            <title>{header}</title>
        </head>
        <body>
        <div class="container">
        <h2 class="d-flex justify-content-center">{header}</h2>
        <div class="mt-5"></div>
        {body}

        <form method="post" action="/create-user" class="input-group mt-2 mb-2 w-50">
         <div class="row g-2 align-items-center">
            <div class="col">
              <input name="username" type="text" class="form-control" placeholder="User name">
            </div>
            <div class="col">
              <input name="age" type="text" class="form-control" placeholder="User age">
            </div>
         
            <div class="col-auto">
              <button type="submit" class="btn btn-primary">Add user</button>
            </div>
          </div>
        </form>

        <form method="post" action="/delete-user" class="input-group mt-2 mb-2 w-50">
         <div class="row g-2 align-items-center">
            <div class="col">
              <input name="username" type="text" class="form-control" placeholder="User name">
            </div>
         
            <div class="col-auto">
              <button type="submit" class="btn btn-primary">Delete user</button>
            </div>
          </div>
        </form>

        <form method="post" action="/update-user" class="mt-2 mb-2 w-75">
          <div class="row g-2 align-items-center">
            <div class="col">
              <input name="username" type="text" class="form-control" placeholder="Current name">
            </div>
            <div class="col">
              <input name="new-username" type="text" class="form-control" placeholder="New name">
            </div>
            <div class="col">
              <input name="new-age" type="text" class="form-control" placeholder="New age">
            </div>
            <div class="col-auto">
              <button type="submit" class="btn btn-primary">Update user</button>
            </div>
          </div>
        </form>

        <form method="post" action="/search-user" class="mt-2 mb-2 w-50">
          <div class="row g-2 align-items-center">
            <div class="col">
              <input name="username" type="text" class="form-control" placeholder="Name">
            </div>
            <div class="col-auto">
              <button type="submit" class="btn btn-primary">Find user</button>
            </div>            
          </div>
        </form>

         <form method="post" class="dropdown">
          <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
            Actions
          </button>
          <ul class="dropdown-menu">
            <li>
              <button type="submit" formaction="/sort-by-age" class="dropdown-item">Sort by age</button>
            </li>
          </ul>
        </form>
        
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/js/bootstrap.bundle.min.js"
        integrity="sha384-ENjdO4Dr2bkBIFxQpeoTz1HIcje39Wm4jDKdf19U8gI4ddQ3GYNS7NTKfAdVQSZe" crossorigin="anonymous"></script>
        </div>
        </body>
        </html>
        """;
    return html;
}

app.Run(async (context) => {
    var response = context.Response;

    var request = context.Request;

    response.ContentType = "text/html; charset=utf-8";

    //При переходе на главную страницу, считываем всех пользователей

    if (request.Path == "/")
    {
        List<User> users = new List<User>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            SqlCommand command = new SqlCommand("select Id, Name, Age from Users", connection);

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        users.Add(new User(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
                    }
                }
            }
        }

        await response.WriteAsync(GenerateHtmlPage(BuildHtmlTable(users), "All Users from DataBase"));
    }
    else if (request.Path == "/create-user" && request.Method == "POST")
    {
        var form = await request.ReadFormAsync();
        string username = form["username"];
        string age = form["age"];

        await CreateUser(username, age, connectionString);
        response.Redirect("/");
        return;
    }
    else if (request.Path == "/delete-user" && request.Method == "POST")
    {
        var form = await request.ReadFormAsync();
        string username = form["username"];


        await DeleteUser(username, connectionString);
        response.Redirect("/");
        return;
    }
    else if (request.Path == "/update-user" && request.Method == "POST")
    {
        var form = await request.ReadFormAsync();
        string username = form["username"];
        string newUserName = form["new-username"];
        string newUserAge = form["new-age"];

        await UpdateUser(username, newUserName,newUserAge, connectionString);
        response.Redirect("/");
        return;
    }
    else if (request.Path == "/search-user" && request.Method == "POST")
    {
        var form = await request.ReadFormAsync();
        string username = form["username"];

        List<User> users = await FindUser(username, connectionString);
        string table = BuildHtmlTable(users);

        await response.WriteAsync(GenerateHtmlPage(table, $"Search results for '{username}'"));
    }
    else if (request.Path == "/sort-by-age" && request.Method == "POST")
    {
        var form = await request.ReadFormAsync();
        string sort = form["sort-by-age"];

        List<User> users = await SortUsers(connectionString);
        string table = BuildHtmlTable(users);

        await response.WriteAsync(GenerateHtmlPage(table, $"Sort '{users}'"));
    }
    else
    {
        response.StatusCode = 404;
        await response.WriteAsJsonAsync("Page Not Found");
    }
});


app.Run();

record User(int Id, string Name, int Age)
{
    public User(string Name, int Age) : this(0, Name, Age) { }
}

