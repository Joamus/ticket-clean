namespace Application.Endpoints.Tickets.ListTickets;

public record ListTicketsRequest
{
	public Guid? Customer { get; set; }
}