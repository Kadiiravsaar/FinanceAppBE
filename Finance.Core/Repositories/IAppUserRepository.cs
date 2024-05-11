using Finance.Core.DTOs.User;
using Finance.Core.Models;

namespace Finance.Core.Repositories
{
    public interface IAppUserRepository : IGenericRepository<AppUser>
	{
		Task<AppUser> SignUp(RegisterDto registerDto);
		Task<AppUser> SignIn(LoginDto loginDto);
		//Task<bool> CheckPassword(AppUser user, string password);
		//Task<AppUser> FindUserByUserName(string userName);
		Task<List<UserDto>> UserList();
	}

}
