using AutoMapper;
using Finance.API.Extensions;
using Finance.API.Helpers;
using Finance.Core.DTOs.Result;
using Finance.Core.DTOs.Stock;
using Finance.Core.Models;
using Finance.Core.Repositories;
using Finance.Core.Services;
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
		private readonly IPortfolioService _portfolioService;
		private readonly IMapper _mapper;
		public PortfoliosController(IMapper mapper, IPortfolioService portfolioService)
		{
			_mapper = mapper;
			_portfolioService = portfolioService;
		}


		[HttpGet]
		[Authorize]
		public async Task<ActionResult> GetUserPortfolio()
		{
			var userPortfolio = await _portfolioService.GetUserPortfolio();
			var userPortfolioDtos = _mapper.Map<List<StockWithCommentDto>>(userPortfolio.Data);
			return Ok(CustomResponseDto<List<StockWithCommentDto>>.Success(200, userPortfolioDtos));
		}


		[HttpPost]
		[Authorize]
		public async Task<IActionResult> AddPortfolio(string symbol)
		{
			var result = await _portfolioService.CreateAsync(symbol);
			return Ok(result);
		}


		[HttpDelete]
		[Authorize]
		public async Task<IActionResult> CreatePortfolio(string symbol)
		{
			var result = await _portfolioService.DeleteAsync(symbol);
			return Ok(result);
		}

	}
}