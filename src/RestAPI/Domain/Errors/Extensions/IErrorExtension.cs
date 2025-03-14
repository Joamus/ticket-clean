using System.Text.Json;
using FluentResults;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Domain.Errors;

public static class IErrorExtension
{
	private const string StatusCode = "StatusCode";

	public static JsonHttpResult<string> ToTypedResult(this IList<IError> errors)
	{
		// For now, we only have multiple errors if it's due to a validation problem. Else will fail fast, i.e at the first error-case.
		int parsedErrorType = (int)ApiErrorType.InternalError;

		var resultErrors = errors.Select(error =>
		{
			return new
			{
				message = error.Message,
				details = error.Metadata
			};
		});

		if (errors.Count == 1)
		{
			IError error = errors.FirstOrDefault()!;

			var match = error.Metadata.First(element => element.Key == StatusCode);
			string value = match.Value.ToString() ?? "";

			int.TryParse(value, out parsedErrorType);
		}
		else if (errors.Count > 1)
		{
			parsedErrorType = (int)ApiErrorType.Validation;
		}


		return TypedResults.Json(JsonSerializer.Serialize(resultErrors), new JsonSerializerOptions(), null, parsedErrorType);
	}
}