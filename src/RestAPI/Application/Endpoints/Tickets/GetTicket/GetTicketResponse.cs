using System.Text.Json.Serialization;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Application.Endpoints.Tickets.GetTicket;

public record GetTicketResponse
{
	public Guid Id { get; set; }

	public long RefId { get; set; }

	public required string Name { get; set; }

	public required string Description { get; set; }

	[JsonConverter(typeof(EnumToStringConverter<State>))]
	public required State State { get; set; }

	public ICollection<TicketComment> Comments { get; set; } = [];

	public (Guid Id, string Name) CreatedBy { get; set; }

	public string? CreatedByName { get; set; }

	public DateTime CreatedAt { get; set; }

	public string? UpdatedByName { get; set; }

	public DateTime UpdatedAt { get; set; }
};