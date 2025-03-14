namespace Application.Auth;
public static class Role
{

	public readonly static IReadOnlyList<string> Roles = new List<string> {
		Customer,
		Supporter
	};

	public const string Customer = "Customer";
	public const string Supporter = "Supporter";
}
