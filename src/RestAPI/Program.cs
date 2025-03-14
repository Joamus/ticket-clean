using Application.Auth;
using Domain.Entities;
using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presentation.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Not super elegant, but works for proof-of-concept - should not use an SQLite database in production.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new FileNotFoundException("No database connection string");

builder.Services.AddCors();

builder.Services.AddAuthorization(options =>
{
    AuthorizationSetupService.SetupClaims(options);
});

builder.Services.AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme, options =>
    {
        options.BearerTokenExpiration = TimeSpan.FromSeconds(600);
    });

builder.Services
    .AddIdentityCore<User>()
    .AddRoles<IdentityRole>()
    .AddUserManager<ApiUserManager>();

builder.Services.AddAppAuthServices();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await AuthorizationSetupService.SetupTestUsers(app);
}

app.UseHttpsRedirection();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

await AuthorizationSetupService.SetupRoles(app);
// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast")
// .WithOpenApi();

app
    .AddAuthEndpoints()
    .AddTicketEndpoints();

await app.RunAsync();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
