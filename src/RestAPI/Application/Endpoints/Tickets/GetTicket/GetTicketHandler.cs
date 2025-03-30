using System.Security.Claims;
using Application.Auth;
using Domain.Entities;
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
			return Result.Fail(ApiError.NotFound("id", "ID not found"));
		}


		Ticket? _result = await _dbContext.Tickets
			.Include(ticket => ticket.CreatedBy)
		// .Select(ticket =>
		// 	new GetTicketResponse
		// 	{
		// 		Name = ticket.Name,
		// 		Description = ticket.Description,
		// 		State = ticket.State,
		// 		Comments = ticket.Comments,
		// 		CreatedAt = ticket.CreatedAt,
		// 		CreatedByName = ticket.CreatedBy
		// 	}
		.Where(ticket => ticket.Id.Equals(_id)).FirstAsync();

		var result =
				new GetTicketResponse
				{
					Name = _result.Name,
					Description = _result.Description,
					State = _result.State,
					Comments = _result.Comments,
					CreatedAt = _result.CreatedAt,
					// CreatedByName = result.CreatedBy
				};
		if (result == null)
		{
			return Result.Fail(ApiError.NotFound());
		}

		return Result.Ok(result);

	}

}