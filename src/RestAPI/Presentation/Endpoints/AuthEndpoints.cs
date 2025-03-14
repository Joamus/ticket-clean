namespace Presentation.Endpoints;

public static class AuthEndpoints
{
	public static IEndpointRouteBuilder AddAuthEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("/auth");


		group.MapPost("login", Login).WithName("Login").WithOpenApi();
		group.MapPost("signup", Signup).WithName("Signup").WithOpenApi();
		group.MapPost("logout", Logout).WithName("Logout").WithOpenApi();


		return app;
	}

	static async Task<IResult> Login()
	{
		return TypedResults.Ok();
	}

	static async Task<IResult> Signup()
	{
		return TypedResults.Ok();
	}

	static async Task<IResult> Logout()
	{
		return TypedResults.Ok();
	}
}