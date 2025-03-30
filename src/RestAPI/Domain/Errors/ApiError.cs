using System.Text.Json.Serialization;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Errors;

public class ApiError : Error
{

	public ApiError(string title, string? description, ApiErrorType errorType)
	{
		Title = title;
		Message = description ?? "";
		ErrorType = errorType;
		// Metadata.Add("StatusCode", (int)errorType);
		// Metadata.Add("Title", title ?? "");
	}

	public string Title { get; init; }

	public ApiErrorType ErrorType { get; init; }

	// [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
	// public new Dictionary<string, object> Metadata { get; } = new();

	#region NotFound
	public static ApiError NotFound()
	{
		return new ApiError("Not found", null, ApiErrorType.NotFound);
	}
	public static ApiError NotFound(string property)
	{
		return new ApiError(property, null, ApiErrorType.NotFound);
	}
	public static ApiError NotFound(string property, string description)
	{
		return new ApiError(property, description, ApiErrorType.NotFound);
	}

	#endregion

	#region Validation
	public static ApiError Validation()
	{
		return new ApiError("Validation", null, ApiErrorType.Validation);
	}
	public static ApiError Validation(string property)
	{
		return new ApiError(property, null, ApiErrorType.Validation);
	}
	public static ApiError Validation(string property, string description)
	{
		return new ApiError(property, description, ApiErrorType.Validation);
	}

	#endregion

	#region Conflict
	public static ApiError Conflict()
	{
		return new ApiError("Conflict", null, ApiErrorType.Validation);
	}
	public static ApiError Conflict(string property)
	{
		return new ApiError(property, null, ApiErrorType.Validation);
	}
	public static ApiError Conflict(string property, string description)
	{
		return new ApiError(property, description, ApiErrorType.Validation);
	}

	#endregion

	#region Conflict
	public static ApiError Unauthorized()
	{
		return new ApiError("Unauthorized", null, ApiErrorType.Unauthorized);
	}
	public static ApiError Unauthorized(string property)
	{
		return new ApiError(property, null, ApiErrorType.Unauthorized);
	}
	public static ApiError Unauthorized(string property, string description)
	{
		return new ApiError(property, description, ApiErrorType.Unauthorized);
	}

	#endregion

	#region Conflict
	public static ApiError Forbidden()
	{
		return new ApiError("Forbidden", null, ApiErrorType.Forbidden);
	}
	public static ApiError Forbidden(string property)
	{
		return new ApiError(property, null, ApiErrorType.Forbidden);
	}
	public static ApiError Forbidden(string property, string description)
	{
		return new ApiError(property, description, ApiErrorType.Forbidden);
	}

	#endregion

	#region InternalError
	public static ApiError InternalError(string property, string description)
	{
		return new ApiError(property, description, ApiErrorType.InternalError);
	}


	public ProblemDetails ToProblemDetails()
	{
		return new ProblemDetails
		{
			Title = this.Title,
			Status = (int)this.ErrorType,
			Detail = this.Message,
		};
	}

	#endregion

}
public enum ApiErrorType
{
	InternalError = 500,
	Validation = 422,
	NotFound = 404,
	Conflict = 422,
	Unauthorized = 401,
	Forbidden = 403
}