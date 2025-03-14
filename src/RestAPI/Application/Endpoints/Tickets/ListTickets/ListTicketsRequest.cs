namespace Application.Endpoints.Tickets.ListTickets;

public record ListTicketsRequest
{
	public Guid? customer { get; set; }
}