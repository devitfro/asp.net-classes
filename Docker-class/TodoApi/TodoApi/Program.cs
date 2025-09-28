using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5, // Максимальное количество повторных попыток
            maxRetryDelay: TimeSpan.FromSeconds(30), // Максимальное время ожидания между попытками
            errorCodesToAdd: null // null или пустой список - использует стандартные ошибки для повтора
        );
    });
});

// Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(builder.Configuration["Redis:Configuration"]));
builder.Services.AddSingleton<RedisCacheService>();


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    db.Database.Migrate();
    if (!db.Tasks.Any())
    {
        db.Tasks.AddRange(
            new TaskItem { Title = "Sample task 1" },
            new TaskItem { Title = "Sample task 2", Description = "Details..." }
        );
        db.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
