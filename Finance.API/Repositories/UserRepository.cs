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
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IMapper _mapper;
		private readonly ITokenService _tokenService;



		public UserRepository(UserManager<AppUser> userManager, IMapper mapper, ITokenService tokenService, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_mapper = mapper;
			_tokenService = tokenService;
			_signInManager = signInManager;
		}

		public async Task<NewUserDto> SignIn(LoginDto loginDto)
		{
			if (loginDto == null)
			{
				throw new ArgumentNullException(nameof(loginDto));
			}

			var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());
			if (user == null) return null;

			var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
			if (result.Succeeded)
			{

				var userInfo = new NewUserDto
				{
					UserName = user.UserName,
					Email = user.Email,
					Token = _tokenService.CreateToken(user)
				};
				return userInfo;
			}
			else
			{
				throw new Exception("Geçersiz kullanıcı adı veya şifre."); // Giriş başarısız olduğunda istenilen işlemin yapılması gerekir.
			}

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
