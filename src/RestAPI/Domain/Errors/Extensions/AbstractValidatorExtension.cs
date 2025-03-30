using Domain.Errors;
using FluentValidation.Results;

namespace Domain.Errors.Extensions;

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