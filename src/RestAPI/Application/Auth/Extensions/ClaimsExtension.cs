namespace System.Security.Claims;
public static class ClaimsExtension
{
	public static string GetUserId(this ClaimsPrincipal principal)
	{
		return principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
	}

}