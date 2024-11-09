using System.Security.Claims;

namespace MantineAdmin.Extensions;

public static class ClaimsExtensions
{
    public static string GetUserName(this ClaimsPrincipal principal)
    {
        return principal.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value;
    }
}