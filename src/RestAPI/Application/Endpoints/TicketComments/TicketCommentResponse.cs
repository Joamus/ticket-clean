namespace Application.Endpoints.TicketComments;

public record TicketCommentResponse
{
	public required string Content { get; set; }

	public required string AuthorName { get; init; }

	public required string AuthorEmail { get; init; }

	public Guid CreatedAt { get; set; }
}