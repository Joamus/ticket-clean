using System.Net;
using System.Security.Claims;
using Application.Auth;
using Application.Endpoints.Tickets;
using Application.Endpoints.Tickets.GetTicket;
using Application.Endpoints.Tickets.ListTickets;
using Application.Errors;
using Domain.Errors;
using FluentResults;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Endpoints;

public static class TicketEndpoints
{
	public static IEndpointRouteBuilder AddTicketEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("/ticket");

		// TODO: Add
		// group.RequireAuthorization();

		group.MapGet("", List).WithName(nameof(List)).WithOpenApi();
		group.MapGet("/{id}", Get).WithName(nameof(Get)).WithOpenApi()
			.Produces<GetTicketResponse>();
		group.MapPost("", Create).WithName(nameof(Create)).WithOpenApi();
		group.MapPost("/{id}", Update).WithName(nameof(Update)).WithOpenApi();
		group.MapDelete("/{id}", Delete).WithName(nameof(Delete)).WithOpenApi();

		group.WithOpenApi();

		return app;
	}

	static async Task<IResult> List([FromQuery] Guid? customer, AppDbContext dbContext, CancellationToken cancellationToken)
	{
		ListTicketsRequest request = new ListTicketsRequest { Customer = customer };
		ListTicketsHandler handler = new(request, dbContext, cancellationToken);

		var result = await handler.HandleAsync();

		if (result.IsSuccess)
		{
			return TypedResults.Ok(result.Value);
		}

		return result.Errors.ToTypedResult();
	}

	// static async Task<IResult> Get(Guid? id, AppDbContext dbContext, HttpContext httpContext, AppAuthService authService)
	static async Task<IResult> Get(HttpRequest httpRequest, AppDbContext dbContext, HttpContext httpContext, AppAuthService authService)
	{
		Guid id;

		string routeId = httpRequest.RouteValues["id"]?.ToString() ?? "";

		if (!Guid.TryParseExact(routeId, null, out id))
		{
			return TypedResults.NotFound(ApiError.NotFound("id", "ID not found"));
		}

		ClaimsPrincipal claims = httpContext.User;

		GetTicketHandler handler = new(id, dbContext, claims, authService);

		var result = await handler.HandleAsync();

		if (result.IsSuccess)
		{
			return TypedResults.Ok(result.Value);
		}

		return result.Errors.ToTypedResult();
	}

	static async Task<IResult> Create(
		[FromBody] CreateTicketRequest request, AppDbContext dbContext,
		HttpContext httpContext, AppAuthService authService)
	{
		// CreteTicketHandler
		return TypedResults.Ok();

	}
	static async Task<IResult> Update([FromRoute] Guid id)
	{
		return TypedResults.Ok();
	}
	static async Task<IResult> Delete([FromRoute] Guid id)
	{
		return TypedResults.Ok();

	}
}