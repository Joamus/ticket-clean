using Domain.Entities;
using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Application.Auth;
public class Role : IdentityRole<Guid>
{
	public Role(string name)
	{
		this.Name = name;
	}

	public readonly static IReadOnlyList<string> Roles = new List<string> {
		Customer,
		Supporter
	};

	public const string Customer = "Customer";
	public const string Supporter = "Supporter";
}