using FluentResults;

namespace Domain.Errors;

public class ApiError : IError
{

	public ApiError(string path, string? description, ApiErrorType errorType)
	{
		Path = path;
		Message = description ?? "";
		Metadata.Add("StatusCode", (int)errorType);
		Metadata.Add("Path", path ?? "");
	}

	public List<IError> Reasons => new List<IError>();

	public string Path { get; init; }

	public string Message { get; init; }

	public Dictionary<string, object> Metadata => new Dictionary<string, object>();

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