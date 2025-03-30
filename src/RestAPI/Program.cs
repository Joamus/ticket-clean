using Application.Auth;
using Application.Auth.Extensions;
using Applicatoin.Notifications.Extensions;
using Domain.Entities;
using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Presentation.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TicketSharp API",
        Description = "Great place to complain",
        Version = "v1"
    });
});


// Not super elegant, but works for proof-of-concept - should not use an SQLite database in production.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new FileNotFoundException("No database connection string");

builder.Services.AddCors();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite(connectionString);
});


// Error handling

builder.Services
.AddGlobalErrorHandling();

builder.Services
.AddAuthorization(AuthorizationSetupService.SetupClaims)
.AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme, options =>
    {
        options.BearerTokenExpiration = TimeSpan.FromSeconds(600);
    });

builder.Services
    .AddNotificationServices()
    .AddAppAuthServices()
    .AddIdentity<User, Role>(opt =>
    {
        opt.SignIn.RequireConfirmedAccount = false;
        opt.SignIn.RequireConfirmedPhoneNumber = false;
        opt.SignIn.RequireConfirmedEmail = false;
        opt.User.RequireUniqueEmail = true;
    })
    .AddRoles<Role>()
    .AddUserManager<ApiUserManager>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
    ;

var app = builder.Build();


await AuthorizationSetupService.SetupRoles(app);

bool isDevelopment = app.Environment.IsDevelopment();
// Configure the HTTP request pipeline.
if (isDevelopment)
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "TicketSharp API V1");
    });
    await AuthorizationSetupService.SetupTestUsers(app);
}
else
{
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseGlobalErrorHandling(isDevelopment);


app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapIdentityApi<User>();


app
    .AddAuthEndpoints()
    .AddTicketEndpoints();

await app.RunAsync();