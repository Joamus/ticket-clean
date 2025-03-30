using Domain.Entities;
using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Application.Auth;

// public class AppUserRole : IdentityUserRole<Guid> { }

// public class AppUserClaim : IdentityUserClaim<Guid> { }

// public class AppUserLogin : IdentityUserLogin<Guid> { }

// public class AppUserStore : UserStore<User, Role, AppDbContext, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityUserToken<Guid>, IdentityRoleClaim<Guid>>
// {
// 	public AppUserStore(AppDbContext context, IdentityErrorDescriber? describer = null) : base(context, describer)
// 	{
// 	}
// }

