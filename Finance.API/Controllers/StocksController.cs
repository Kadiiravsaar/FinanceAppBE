using AutoMapper;
using Finance.Core.DTOs.Result;
using Finance.Core.DTOs.Stock;
using Finance.Core.Models;
using Finance.Core.Services;
using Finance.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Finance.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class StocksController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IStockService _service;

		public StocksController(IMapper mapper, IStockService stockService)
		{
			_mapper = mapper;
			_service = stockService;
		}

		[HttpGet]
		public async Task<IActionResult> All()
		{
			var stocks = await _service.GetAllAsync();
			var stocksDtos = _mapper.Map<List<StockWithCommentDto>>(stocks.ToList());
			return Ok(CustomResponseDto<List<StockWithCommentDto>>.Success(200, stocksDtos));
		}


		[HttpGet("allwithcomments")]
		public async Task<IActionResult> GetAllWithComments()
		{
			var stocks = await _service.GetAllWithCommentsAsync();
			return Ok(CustomResponseDto<List<StockWithCommentDto>>.Success(200, stocks.Data));
		}


		[HttpPost]
		public async Task<IActionResult> AddStock([FromBody] CreateStockRequestDto stockRequestDto)
		{
			var stock = await _service.AddAsync(_mapper.Map<Stock>(stockRequestDto));
			var stocksDto = _mapper.Map<CreateStockRequestDto>(stock);
			return Ok(CustomResponseDto<CreateStockRequestDto>.Success(201, stocksDto));
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var stock = await _service.GetByIdAsync(id);
			await _service.RemoveAsync(stock);
			return Ok(CustomResponseDto<NoContentDto>.Success(200));
		}


		[HttpPut]
		[Route("{id:int}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
		{
			var stock = await _service.GetByIdAsync(id);
			await _service.UpdateAsync(_mapper.Map(updateStockRequestDto, stock));
			return Ok(CustomResponseDto<NoContentDto>.Success(200));
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			var stock = await _service.GetByIdAsync(id);
			var stockDto = _mapper.Map<StockWithCommentDto>(stock);
			return Ok(CustomResponseDto<StockWithCommentDto>.Success(201, stockDto));
		}

		[HttpGet("getallfiltering")]
		public async Task<IActionResult> GetStocksAsync([FromQuery]QueryObject queryObject)
		{
			var stocks = await _service.GetStocksAsync(queryObject);
			return Ok(CustomResponseDto<List<StockWithCommentDto>>.Success(200, stocks.Data));
		}


	}
}
