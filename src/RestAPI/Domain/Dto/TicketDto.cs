namespace Domain.Dto;

public record TicketDto
{
	public required string Name { get; init; }

	public required string Description { get; init; }

	public required long RefId { get; init; }
}