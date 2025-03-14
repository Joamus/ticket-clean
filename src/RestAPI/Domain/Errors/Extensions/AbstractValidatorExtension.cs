using Domain.Errors;
using FluentResults;
using FluentValidation.Results;

namespace Domain.Erors.Extensions;

public static class AbstractValidatorExtension
{
	public static List<ApiError> ToApiErrors(this ValidationResult result)
	{
		List<ApiError> errors = new List<ApiError>();

		foreach (var error in result.Errors)
		{
			errors.Add(ApiError.Validation(error.PropertyName, error.ErrorMessage));
		}

		return errors;
	}
}