using AutoMapper;
using Finance.API.Dtos.User;
using Finance.API.Interfaces;
using Finance.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Finance.API.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IMapper _mapper;


		public UserRepository(UserManager<AppUser> userManager, IMapper mapper)
		{
			_userManager = userManager;
			_mapper = mapper;
		}

		public async Task<AppUser> SignUp(RegisterDto registerDto)
		{
			if (registerDto == null)
			{
				throw new ArgumentNullException(nameof(registerDto));
			}
			var user = new AppUser()
			{
				UserName = registerDto.UserName,
				Email = registerDto.Email

			};
			var result = await _userManager.CreateAsync(user, registerDto.Password);

			if (result.Succeeded)
			{
			var role = await _userManager.AddToRoleAsync(user, "User");


				return user;

			}
			else
			{
				throw new Exception("Kullanıcı oluşturulamadı");
			}
		}

		public async Task<List<UserDto>> UserList()
		{
			var users = await _userManager.Users.ToListAsync();
			var dtos = _mapper.Map<List<UserDto>>(users);
			return dtos;
			
		}

	}
}
