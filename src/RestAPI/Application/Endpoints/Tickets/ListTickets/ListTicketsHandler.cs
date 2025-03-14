using System.Reflection.Metadata;
using FluentResults;
using Infrastructure.Database;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Endpoints.Tickets;
using Microsoft.Extensions.ObjectPool;
using Domain.Errors;

namespace Application.Endpoints.Tickets.ListTickets;
public class ListTicketsHandler
{
	private readonly ListTicketsRequest _request;
	private readonly AppDbContext _dbContext;

	private readonly CancellationToken _cancellationToken;
	public ListTicketsHandler(ListTicketsRequest request, AppDbContext dbContext, CancellationToken cancellationToken)
	{
		_request = request;
		_dbContext = dbContext;
		_cancellationToken = cancellationToken;
	}

	public async Task<Result<IList<ListTicketResponse>>> HandleAsync()
	{

		var validateResult = Validate();

		if (validateResult.IsFailed)
		{
			return validateResult;
		}

		IList<ListTicketResponse> tickets = await _dbContext.Tickets.Select(ticket =>
		new ListTicketResponse
		{
			Id = ticket.Id,
			Name = ticket.Name,
			RefId = ticket.RefId,
			Description = ticket.Description,
			State = ticket.State,
		}
		).ToListAsync(_cancellationToken);

		return Result.Ok(tickets);
	}

	public Result Validate()
	{
		if (_request.customer is not null)
		{
			return Result.Ok();
		}
		else
		{
			// TODO: Implement
			return Result.Fail(ApiError.Conflict("customer", "Customer filtering not implemented"));
		}
	}

}