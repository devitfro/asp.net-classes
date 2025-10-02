using app_hw.Mapping;
using AutoMapper;
using app_hw.Mapping;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddSession();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.UseSession();

app.MapDefaultControllerRoute();
app.Run();
