namespace Application.Auth;
public static class Policy
{

	public readonly static IReadOnlyList<string> Roles = new List<string> {
		Employee,
		MinGuest,
		MinSupporter
	};

	public const string Employee = "Employee";
	public const string MinGuest = "MinGuest";
	public const string MinSupporter = "MinSupporter";
}
