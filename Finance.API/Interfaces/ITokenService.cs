using Finance.API.Models;

namespace Finance.API.Interfaces
{
	public interface ITokenService
	{
		string CreateToken(AppUser appUser);
	}
}
