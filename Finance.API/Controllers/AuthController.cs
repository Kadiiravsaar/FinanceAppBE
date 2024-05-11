using AutoMapper;
using Finance.Core.DTOs.Result;
using Finance.Core.DTOs.User;
using Finance.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IMapper _mapper;

		public AuthController(IUserService userService, IMapper mapper)
		{
			_userService = userService;
			_mapper = mapper;
		}


		[HttpPost]
		public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
		{
			var user = await _userService.SignUp(registerDto);
			return Ok(user);

		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
		{
			var user = await _userService.SignIn(loginDto);
			return Ok(user);

		}

		[HttpGet]
		public async Task<IActionResult> GetAllUsers()
		{
			var userList = await _userService.UserList();
			return Ok(userList);
		}
	}
}
