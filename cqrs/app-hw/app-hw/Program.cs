using Microsoft.EntityFrameworkCore;
using MediatR;
using Library.Api.Data;
using app_hw.Data;

var builder = WebApplication.CreateBuilder(args);

// add services
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationContext>(opt => opt.UseInMemoryDatabase("LibraryDb"));
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    // seed demo data
    if (!db.Books.Any())
    {
        db.Books.AddRange(
            new app_hw.Models.Book { Title = "1", Author = "Тол1стой", Year = 1869 },
            new app_hw.Models.Book { Title = "1", Author = "2", Year = 1967 }
        );
        db.SaveChanges();
    }
}

app.Run();
