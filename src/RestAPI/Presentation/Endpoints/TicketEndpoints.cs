using System.Security.Claims;
using Application.Auth;
using Application.Endpoints.Tickets;
using Application.Endpoints.Tickets.GetTicket;
using Application.Endpoints.Tickets.ListTickets;
using Application.Tickets.DTO;
using Domain.Entities;
using Domain.Errors;
using Infrastructure.Database;

namespace Presentation.Endpoints;

public static class TicketEndpoints
{
	public static IEndpointRouteBuilder AddTicketEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("/ticket");

		// TODO: Add
		// group.RequireAuthorization();

		group.MapGet("", List).WithName(nameof(List)).WithOpenApi();
		group.MapGet("{id}", Get).WithName(nameof(Get)).WithOpenApi();
		group.MapPost("", Create).WithName(nameof(Create)).WithOpenApi();
		group.MapPost("/{id}", Update).WithName(nameof(Update)).WithOpenApi();
		group.MapDelete("/{id}", Delete).WithName(nameof(Delete)).WithOpenApi();

		group.WithOpenApi();

		return app;
	}

	static async Task<IResult> List(ListTicketsRequest request, AppDbContext dbContext, CancellationToken cancellationToken)
	{
		ListTicketsHandler handler = new(request, dbContext, cancellationToken);

		var result = await handler.HandleAsync();

		if (result.IsSuccess)
		{
			return TypedResults.Ok(result.Value);
		}

		return result.Errors.ToTypedResult();
	}

	static async Task<IResult> Get(Guid id, AppDbContext dbContext, HttpContext httpContext, AppAuthService authService)
	{
		ClaimsPrincipal claims = httpContext.User;

		GetTicketHandler handler = new(id, dbContext, claims, authService);

		var result = await handler.HandleAsync();

		if (result.IsSuccess)
		{
			return TypedResults.Ok(result.Value);
		}

		return result.Errors.ToTypedResult();
	}

	static async Task<IResult> Create(CreateTicketRequest request, AppDbContext dbContext, HttpContext httpContext, AppAuthService authService)
	{
		// CreteTicketHandler
		return TypedResults.Ok();

	}
	static async Task<IResult> Update(Guid id)
	{
		return TypedResults.Ok();
	}
	static async Task<IResult> Delete(Guid id)
	{
		return TypedResults.Ok();

	}
}