﻿using Microsoft.EntityFrameworkCore;
using API_Template.Data.DataBase.Context;
using API_Template.Api.SeedData;
using API_Template.Api.Extensions;
using API_Template.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAppServices(builder.Configuration);

builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(u => u.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:7051", "http://localhost:5257"));

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);

}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "حدث خطاء اثناء ترحيل البيانات");
}

app.Run();
