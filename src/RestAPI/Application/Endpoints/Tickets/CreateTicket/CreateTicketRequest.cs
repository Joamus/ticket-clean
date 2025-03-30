namespace Application.Endpoints.Tickets;

public class CreateTicketRequest
{
	public required string Name { get; set; }

	public required string Description { get; set; }
}