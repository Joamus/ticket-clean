using System.Security.Claims;
using Domain.Entities;
using Domain.Errors;
using FluentResults;
using Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Application.Auth;

public class AppAuthService
{
	private readonly IAuthorizationService _authService;
	private readonly AppDbContext _dbContext;

	public AppAuthService(IAuthorizationService authService, AppDbContext dbContext)
	{
		_authService = authService;
		_dbContext = dbContext;
	}

	public async ValueTask<bool> HasReadAccess(ClaimsPrincipal claims, Guid targetUserId)
	{
		try
		{
			Guid requestingUserId = Guid.Parse(claims.GetUserId());

			if (requestingUserId.Equals(targetUserId))
			{
				return true;
			}

			return (await _authService.AuthorizeAsync(claims, Policy.MinSupporter)).Succeeded;

		}
		catch (Exception _e)
		{
			return false;
		}
	}
	public async ValueTask<bool> HasWriteAccess(ClaimsPrincipal claims, Guid targetUserId)
	{
		try
		{
			Guid requestingUserId = Guid.Parse(claims.GetUserId());

			if (requestingUserId.Equals(targetUserId))
			{
				return true;
			}

			return (await _authService.AuthorizeAsync(claims, Policy.MinSupporter)).Succeeded;

		}
		catch (Exception _e)
		{
			return false;
		}
	}

	public async Task<Result<User?>> GetRequestingUserAsync(ClaimsPrincipal claims)
	{
		Guid requestingUserId = Guid.Parse(claims.GetUserId());

		var user = await _dbContext.Users.FindAsync(requestingUserId);

		return Result.Ok(user);
	}

	public async Task<Result<User?>> GetUserAsync(ClaimsPrincipal claims, Guid userId)
	{
		if (!await HasReadAccess(claims, userId))
		{
			return Result.Fail(ApiError.Unauthorized());
		}

		var user = await _dbContext.Users.FindAsync(userId);

		return Result.Ok(user);
	}

}