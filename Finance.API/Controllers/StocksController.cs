﻿using AutoMapper;
using Finance.API.Data;
using Finance.API.Dtos.Stock;
using Finance.API.Helpers;
using Finance.API.Interfaces;
using Finance.API.Models;
using Finance.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Finance.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StocksController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
		private readonly IStockRepository _stockRepository;
		public StocksController(AppDbContext context, IMapper mapper, IStockRepository stockRepository)
		{
			_context = context;
			_mapper = mapper;
			_stockRepository = stockRepository;
		}


		[HttpGet("all")]
		public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var stocks = await _stockRepository.GetAllAsync(queryObject);
			var stockDtos = _mapper.Map<List<StockDto>>(stocks);
			return Ok(stockDtos);
		}

		[HttpGet("allwithcomments")]
		public async Task<IActionResult> GetAllWithComments()
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var stocks = await _stockRepository.GetAllWithCommentsAsync();
			var stockDtos = _mapper.Map<List<StockDto>>(stocks);
			return Ok(stockDtos);
		}


		// FromRoute https://api.myservice.com/books/123
		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var stock = await _stockRepository.GetByIdAsync(id);
			if (stock == null) return NotFound();
			var stockDto = _mapper.Map<StockDto>(stock);
			return Ok(stockDto);

		}


		[HttpPost]
		public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDto stockRequestDto)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var stockModel = _mapper.Map<Stock>(stockRequestDto);
			await _stockRepository.CreateAsync(stockModel);
			return Ok(stockRequestDto);
		}


		[HttpPut]
		[Route("{id:int}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var stockModel = await _stockRepository.UpdateAsync(id, updateStockRequestDto);
			if (stockModel == null) return NotFound();
			return Ok(updateStockRequestDto);

			//_mapper.Map<Stock>(updateStockRequestDto); değişkene atamadığım için kullanmıyorum
			//_mapper.Map(updateStockRequestDto, stockModel);
		}

		[HttpDelete]
		[Route("{id:int}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var stockModel = await _stockRepository.DeleteAsync(id);
			if (stockModel == null) return NotFound();
			return NoContent();
		}
	}
}