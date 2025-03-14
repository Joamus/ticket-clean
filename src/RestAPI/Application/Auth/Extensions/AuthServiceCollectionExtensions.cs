using Application.Auth;

public static class AuthServiceCollectionExtensions
{
	public static IServiceCollection AddAppAuthServices(this IServiceCollection services)
	{
		services.AddScoped<AppAuthService, AppAuthService>();

		return services;
	}
}