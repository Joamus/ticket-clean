using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Application.Auth;

public class ApiUserManager : UserManager<User>
{

	public ApiUserManager(
		IUserStore<User> store,
		IOptions<IdentityOptions> optionsAccessor,
		IPasswordHasher<User> passwordHasher,
		IEnumerable<IUserValidator<User>> userValidators,
		IEnumerable<IPasswordValidator<User>> passwordValidators,
		ILookupNormalizer keyNormalizer,
		IdentityErrorDescriber errors,
		IServiceProvider services,
		ILogger<ApiUserManager> logger) :
			base(
				store,
				optionsAccessor,
				passwordHasher,
				userValidators,
				passwordValidators,
				keyNormalizer,
				errors,
				services,
				logger
			)
	{
	}

	public async override Task<IdentityResult> CreateAsync(User user)
	{
		await base.CreateAsync(user);

		return await AddToRoleAsync(user, Role.Customer);
	}
}