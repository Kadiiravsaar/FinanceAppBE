using System.Security.Claims;

namespace Finance.API.Extensions
{
	public static class ClaimExtensions
	{
        public static string GetUserId(this ClaimsPrincipal user)
        {
			var claim =  user.Claims.SingleOrDefault
				(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"));
			return claim!.Value;
		}
	}
}
