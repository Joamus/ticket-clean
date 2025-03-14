using Domain.Dto;
using Domain.Erors.Extensions;
using Domain.Errors;
using FluentResults;
using FluentValidation;

namespace Domain.Entities;

public class Ticket
{

	private static TicketValidator _validator = new TicketValidator();

	private Ticket()
	{ }

	public Guid Id { get; init; }

	public long RefId { get; init; }

	public string Name { get; private set; }

	public string Description { get; private set; }

	public State State { get; private set; }

	private ICollection<TicketComment> _comments;

	public ICollection<TicketComment> Comments { get; private set; }

	public Guid CreatedBy { get; }

	private DateTime CreatedAt { get; }

	public User UpdatedBy { get; private set; }

	public DateTime UpdatedAt { get; private set; }

	public static Result<Ticket> Create(
		User user,
		TicketDto ticketDto
	)
	{
		var errors = Validate(ticketDto);

		if (errors.Count > 0)
		{
			return Result.Fail(errors);
		}

		return Result.Ok(new Ticket());
	}

	private static List<ApiError> Validate(TicketDto ticketDto)
	{
		var result = _validator.Validate(ticketDto);

		return result.ToApiErrors();
	}
}

public enum State
{
	New,
	Answered,
	Closed,

}
public class TicketValidator : AbstractValidator<TicketDto>
{
	public TicketValidator()
	{

		RuleFor(t => t.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
		RuleFor(t => t.Description).NotEmpty().MinimumLength(1).MaximumLength(1000);

	}

}