using AutoMapper;
using Finance.Core.DTOs.User;
using Finance.Core.Models;
using Finance.Core.Repositories;
using Finance.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Finance.Repository.Repositories
{
    public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IMapper _mapper;
		private readonly ITokenService _tokenService;
		public AppUserRepository(AppDbContext context, SignInManager<AppUser> signInManager, IMapper mapper, ITokenService tokenService) : base(context)
		{
			_signInManager = signInManager;
			_mapper = mapper;
			_tokenService = tokenService;
		}

		public async Task<AppUser> SignIn(LoginDto loginDto)
		{

			if (loginDto == null)
			{
				throw new ArgumentNullException(nameof(loginDto));
			}

			var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());
			if (user == null) return null;

			var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
			if (!result.Succeeded)
			{
				throw new Exception("Geçersiz kullanıcı adı veya şifre."); // Giriş başarısız olduğunda istenilen işlemin yapılması gerekir.
			}

			return user;
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
				await _userManager.AddToRoleAsync(user, "User");
				var userDto = new NewUserDto
				{
					UserName = user.UserName,
					Email = user.Email,
					Token = _tokenService.CreateToken(user)
				};

				var appUser = _mapper.Map<AppUser>(userDto);
				return appUser;

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

		//public async Task<AppUser> FindUserByUserName(string userName)
		//{
		//	return await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName.ToLower());
		//}

		//public async Task<bool> CheckPassword(AppUser user, string password)
		//{
		//	return (await _signInManager.CheckPasswordSignInAsync(user, password, false)).Succeeded;
		//}
	}

}
