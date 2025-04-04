
using Domain.Entities;

namespace Application.Endpoints.Tickets;

public class ListTicketResponse
{
	public Guid Id { get; set; }

	public long RefId { get; set; }

	public required string Name { get; set; }

	public required string Description { get; set; }

	public required State State { get; set; }

	public User? CreatedBy { get; set; }

	public DateTime CreatedAt { get; set; }

	public User? UpdatedBy { get; set; }

	public DateTime UpdatedAt { get; set; }
}