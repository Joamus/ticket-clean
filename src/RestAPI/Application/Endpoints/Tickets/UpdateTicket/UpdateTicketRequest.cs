using Domain.Entities;

namespace Application.Tickets.DTO;

public class UpdateTicketRequest
{
	public required string Name { get; set; }

	public required string Description { get; set; }

	public required State State { get; set; }
}