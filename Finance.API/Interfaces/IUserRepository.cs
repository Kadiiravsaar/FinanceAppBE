using Finance.API.Dtos.User;
using Finance.API.Models;

namespace Finance.API.Interfaces
{
	public interface IUserRepository
	{
		Task<NewUserDto> SignUp(RegisterDto registerDto);
		Task<NewUserDto> SignIn(LoginDto loginDto);
		Task<List<UserDto>> UserList();
	}
}
