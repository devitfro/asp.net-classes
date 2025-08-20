using app_hw.Middleware;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var configurationService = app.Services.GetService<IConfiguration>();
string connectionString = configurationService["ConnectionStrings:DefaultConnection"];

app.UseMiddleware<PathMiddleware>();

app.UseMiddleware<LoginMiddleware>();

app.UseMiddleware<SelectBookMiddleware>(connectionString);

app.Run();


