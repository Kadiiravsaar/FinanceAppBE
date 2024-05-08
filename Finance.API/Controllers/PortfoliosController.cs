using Finance.API.Extensions;
using Finance.API.Helpers;
using Finance.API.Interfaces;
using Finance.API.Models;
using Finance.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PortfoliosController : ControllerBase
	{
		private readonly IPortfolioRepository _portfolioRepository;


		public PortfoliosController(IPortfolioRepository portfolioRepository)
		{
			_portfolioRepository = portfolioRepository;
		}

		[HttpGet]
		[Authorize]
		public async Task<ActionResult<List<Stock>>> GetUserPortfolio()
		{
			var userPortfolio = await _portfolioRepository.GetUserPortfolio();
			return Ok(userPortfolio);
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> AddPortfolio(string symbol)
		{
			var result = await _portfolioRepository.CreateAsync(symbol);
			return Ok(result);

		}


		[HttpDelete]
		[Authorize]
		public async Task<IActionResult> CreatePortfolio(string symbol)
		{
			var result = await _portfolioRepository.DeleteAsync(symbol);
			return Ok(result);

		}

	}
}
