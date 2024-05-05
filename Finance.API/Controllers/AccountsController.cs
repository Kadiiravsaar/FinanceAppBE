using Finance.API.Dtos.User;
using Finance.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly IUserRepository _userRepository;

		public AccountsController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		[HttpPost]
		public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
		{
			var user = await _userRepository.SignUp(registerDto);
			return Ok(user);

		}

		[HttpGet]
		public async Task<IActionResult> GetAllUsers()
		{
			var userList =await _userRepository.UserList();
			return Ok(userList);	
		}
	}
}
