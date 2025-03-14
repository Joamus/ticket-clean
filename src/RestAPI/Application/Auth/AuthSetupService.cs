using System.Security.Claims;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth;
public static class AuthorizationSetupService
{
	public static void SetupClaims(AuthorizationOptions options)
	{
		options.AddPolicy(Policy.Employee, policy => policy.RequireClaim("EmployeeNumber"));
		options.AddPolicy(Policy.MinGuest, policy => policy.RequireRole(Role.Customer, Role.Supporter));
		options.AddPolicy(Policy.MinSupporter, policy => policy.RequireRole(Role.Supporter));
	}

	public async static Task SetupRoles(WebApplication app)
	{

		using (var scope = app.Services.CreateScope())
		{
			var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			var roles = Role.Roles;

			foreach (var role in roles)
			{
				if (!await roleManager.RoleExistsAsync(role))
				{
					await roleManager.CreateAsync(new IdentityRole(role));
				}
			}
		}
	}

	public async static Task SetupTestUsers(WebApplication app)
	{
		using (var scope = app.Services.CreateScope())
		{
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
			// Add a default manager

			string managerEmail = "supporter@tick.it";
			string managerPassword = "ILoveTickets1234#@";

			if (await userManager.FindByEmailAsync(managerEmail) == null)
			{
				var user = new User();
				user.Email = managerEmail;
				user.UserName = managerEmail;
				user.PasswordHash = managerPassword;

				await userManager.CreateAsync(user, managerPassword);
				await userManager.AddToRoleAsync(user, Role.Supporter);
			}

			string guestEmail = "guest@tick.it";
			string guestPassword = "ILoveTickets1234#@";

			if (await userManager.FindByEmailAsync(managerEmail) == null)
			{
				var user = new User();
				user.Email = guestEmail;
				user.UserName = guestEmail;
				user.PasswordHash = guestPassword;

				await userManager.CreateAsync(user, guestPassword);
				await userManager.AddToRoleAsync(user, Role.Customer);
			}
		}
	}
}
