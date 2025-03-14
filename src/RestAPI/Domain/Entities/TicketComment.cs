namespace Domain.Entities;

public class TicketComment
{
	public Guid Id { get; set; }

	public Guid TicketId { get; set; }

	public required string Content { get; set; }

	public Guid CreatedBy { get; set; }

	public Guid CreatedAt { get; set; }


}