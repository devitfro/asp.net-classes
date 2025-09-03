using app_class_2.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ISingletonService, SingletonService>();
builder.Services.AddScoped<IScopedService, ScopedService>();
builder.Services.AddTransient<ITransientService, TransientService>();  

var app = builder.Build();

app.UseMiddleware<MyMiddleware>();

app.Run();

public interface ISingletonService
{
    int Id { get; }
}

public interface IScopedService
{
    int Id { get; }
}

public interface ITransientService
{
    int Id { get; }
}

class SingletonService : ISingletonService
{
    public int Id { get; } = new Random().Next(1, 1000);
}

class ScopedService : IScopedService
{
    public int Id { get; } = new Random().Next(1, 1000);
}

class TransientService : ITransientService
{
    public int Id { get; } = new Random().Next(1, 1000);
}

