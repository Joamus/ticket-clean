using DotNext;
using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Errors;
using FluentResults;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Errors;

public static class IErrorExtensions
{
	private const string StatusCode = "StatusCode";

	public static JsonHttpResult<ErrorResponse> ToTypedResult(this IList<IError> errors)
	{


		if (errors.Count == 0)
		{
			// TODO: Handle properly later, maybe give a better response, and notify ops/etc that something is wrong
			throw new ArgumentException("No errors found - should not happen");
		}

		// For now, we only have multiple errors if it's due to a validation problem. Else will fail fast, i.e at the first error-case.
		int parsedErrorType = (int)ApiErrorType.InternalError;


		if (errors is not List<ApiError>)
		{
			IError _mainError = errors.FirstOrDefault()!;

			var _title = _mainError.Metadata.FirstOrDefault(element => element.Key == "Title");

			var _response = new ErrorResponse
			{
				Title = _title.Value?.ToString() ?? "",
				Status = parsedErrorType,
				Detail = _mainError.Message,
			};

			return TypedResults.Json(_response, new JsonSerializerOptions(), null, parsedErrorType);
		}

		List<OutputError> innerErrors = errors.Select(error =>
			{
				return new OutputError
				(
					error.Message,
					error.Metadata
				);
			}).ToList();

		ApiError mainError = (ApiError)errors.FirstOrDefault()!;

		string value = "";

		try
		{
			var match = mainError.Metadata.First(element => element.Key == StatusCode);
			value = match.Value?.ToString() ?? "";
		}
		catch (Exception _e)
		{
			value = "";
		}

		if (!string.IsNullOrEmpty(value))
		{
			int.TryParse(value, out parsedErrorType);
		}

		var title = mainError.Metadata.FirstOrDefault(element => element.Key == "Title");

		var response = new ErrorResponse
		{
			Title = title.Value?.ToString() ?? "",
			Status = parsedErrorType,
			Detail = mainError.Message,
		};

		if (errors.Count > 1)
		{
			response.Title = "Multiple errors - see inside";
			response.errors = innerErrors;
		}

		return TypedResults.Json(response, new JsonSerializerOptions(), null, parsedErrorType);
	}

	// internal static TypedResultFromAPIErrors()

}

public sealed class ErrorResponse : ProblemDetails
{

	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<OutputError>? errors { get; set; }
};
public sealed record OutputError(string message, Dictionary<string, object> details);