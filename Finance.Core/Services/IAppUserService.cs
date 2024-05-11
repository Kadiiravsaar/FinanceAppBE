using Finance.Core.DTOs.Result;
using Finance.Core.DTOs.User;
using Finance.Core.Models;

namespace Finance.Core.Services
{
    public interface IAppUserService : IService<AppUser>
	{
		Task<CustomResponseDto<AppUser>> SignUp(RegisterDto registerDto);
		Task<CustomResponseDto<AppUser>> SignIn(LoginDto loginDto);
	}

}
