using System.Security.Claims;
using Application.Auth;
using Domain.Errors;
using FluentResults;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Application.Endpoints.Tickets.GetTicket;
public class GetTicketHandler
{
	private readonly Guid _id;

	private readonly AppDbContext _dbContext;

	private readonly ClaimsPrincipal _claims;

	private readonly AppAuthService _authService;

	public GetTicketHandler(Guid id, AppDbContext dbContext, ClaimsPrincipal claims, AppAuthService authService)
	{
		_id = id;
		_dbContext = dbContext;
		_claims = claims;
		_authService = authService;
	}

	public async Task<Result<GetTicketResponse>> HandleAsync()
	{
		if (!await _authService.HasReadAccess(_claims, _id))
		{
			return Result.Fail(ApiError.NotFound());
		}


		GetTicketResponse? result = await _dbContext.Tickets.Select(ticket =>
			new GetTicketResponse
			{
				Name = ticket.Name,
				Description = ticket.Description,
				State = ticket.State,
			}
		).Where(ticket => ticket.Id.Equals(_id)).FirstAsync();

		if (result == null)
		{
			return Result.Fail(ApiError.NotFound());
		}

		return Result.Ok(result);

	}

}