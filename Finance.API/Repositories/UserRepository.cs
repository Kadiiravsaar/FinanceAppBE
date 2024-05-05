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
		private readonly ITokenService _tokenService;



		public UserRepository(UserManager<AppUser> userManager, IMapper mapper, ITokenService tokenService)
		{
			_userManager = userManager;
			_mapper = mapper;
			_tokenService = tokenService;
		}

		public async Task<NewUserDto> SignUp(RegisterDto registerDto)
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
				await _userManager.AddToRoleAsync(user, "User");
				var userDto = new NewUserDto
				{
					UserName = user.UserName,
					Email = user.Email,
					Token = _tokenService.CreateToken(user)
				};

				return userDto;

			}
			else
			{
				foreach (var error in result.Errors)
				{
					Console.WriteLine($"Error: {error.Code} - {error.Description}");
				}

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
