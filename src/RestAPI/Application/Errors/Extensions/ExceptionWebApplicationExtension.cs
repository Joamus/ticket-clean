public static class ExceptionWebApplicatoinExtension
{
	public static WebApplication UseGlobalErrorHandling(this WebApplication app, bool isDevelopment)
	{
		if (!isDevelopment)
		{
			app.UseExceptionHandler();
		}

		return app;
	}
}