using Finance.API.Dtos.User;
using Finance.API.Models;

namespace Finance.API.Interfaces
{
	public interface IUserRepository
	{
		Task<AppUser> SignUp(RegisterDto registerDto);
		Task<List<UserDto>> UserList();
	}
}
