using Application.Auth;

namespace Application.Auth.Extensions;

public static class AuthServiceCollectionExtensions
{
	public static IServiceCollection AddAppAuthServices(this IServiceCollection services)
	{
		services.AddScoped<AppAuthService>();

		return services;
	}
}