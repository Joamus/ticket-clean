using System.Security.Claims;
using Application.Auth;
using Application.Endpoints.TicketComments;
using Domain.Dto;
using Domain.Entities;
using FluentResults;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Application.Endpoints.Tickets.CreateTicket;

public class CreateTicketHandler
{
	private readonly CreateTicketRequest _request;

	private readonly ClaimsPrincipal _claims;

	private readonly AppDbContext _dbContext;
	private readonly AppAuthService _authService;

	private readonly CancellationToken _cancellationToken;

	public CreateTicketHandler(
		CreateTicketRequest request,
		ClaimsPrincipal claims,
		AppDbContext dbContext,
		AppAuthService authService,
		CancellationToken cancellationToken
	)
	{
		_request = request;
		_claims = claims;
		_dbContext = dbContext;
		_authService = authService;
		_cancellationToken = cancellationToken;
	}

	public async Task<Result<CreateTicketResponse>> HandleAsync()
	{
		Result<User> userResult = await _authService.GetRequestingUserAsync(_claims);
		User user = userResult.Value;

		long refId = await GetNextRefIdForUser(user.Id);

		TicketDto ticketDto = new TicketDto
		{
			Name = _request.Name,
			Description = _request.Description,
			RefId = refId
		};

		Result<Ticket> ticketResult = Ticket.Create(user, ticketDto);

		if (ticketResult.IsFailed)
		{
			return Result.Fail(ticketResult.Errors);
		}

		Ticket ticket = ticketResult.Value;

		var result = (await _dbContext.Tickets.AddAsync(ticket, _cancellationToken)).Entity;
		await _dbContext.SaveChangesAsync();


		CreateTicketResponse response = new CreateTicketResponse
		{
			Id = result.Id,
			RefId = refId,
			Name = result.Name,
			Description = result.Description,
			AuthorName = user.Name,
			AuthorEmail = user.Email ?? "",
			CreatedAt = DateTime.UtcNow,
			State = result.State,
			Comments = new List<TicketCommentResponse>()
		};

		return Result.Ok(response);
	}

	private async Task<long> GetNextRefIdForUser(Guid userId)
	{
		var result = await _dbContext.Tickets
			.Where(ticket => ticket.CreatedBy.Equals(userId))
			.Select(ticket => new { RefId = ticket.RefId })
			.OrderByDescending(ticket => ticket.RefId)
			.FirstOrDefaultAsync();

		return result?.RefId ?? 1;
	}
}

