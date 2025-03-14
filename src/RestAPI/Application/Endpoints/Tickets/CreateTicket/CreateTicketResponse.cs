using Application.Endpoints.TicketComments;
using Domain.Entities;

namespace Application.Endpoints.Tickets;

public record CreateTicketResponse
{
	public required Guid Id { get; init; }

	public required long RefId { get; init; }

	public required string Name { get; init; }

	public required string Description { get; init; }

	public required State State { get; init; }

	public required ICollection<TicketCommentResponse> Comments { get; init; }

	public required string AuthorName { get; init; }

	public required string AuthorEmail { get; init; }

	public required DateTime CreatedAt { get; init; }
}