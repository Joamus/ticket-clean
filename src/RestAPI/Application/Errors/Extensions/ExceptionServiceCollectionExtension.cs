public static class ExceptionServiceCollectionExtension
{
	public static IServiceCollection AddGlobalErrorHandling(this IServiceCollection services)
	{
		services.AddProblemDetails();

		return services;
	}
}