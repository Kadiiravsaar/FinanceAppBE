using Finance.Core.DTOs.Result;
using Finance.Core.DTOs.User;
using Finance.Core.Models;

namespace Finance.Core.Services
{
	public interface IUserService 
	{
		Task<NewUserDto> SignUp(RegisterDto registerDto);
		Task<NewUserDto> SignIn(LoginDto loginDto);
		Task<List<UserDto>> UserList();
	}
}


