using AutoMapper;
using Finance.Core.DTOs.Result;
using Finance.Core.DTOs.Stock;
using Finance.Core.Models;
using Finance.Core.Repositories;
using Finance.Core.Services;
using Finance.Core.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Service.Services
{
    public class StockService : Service<Stock>, IStockService
	{

		private readonly IStockRepository _stockRepository;
		private readonly IMapper _mapper;


		public StockService(IGenericRepository<Stock> repository, IUnitOfWork unitOfWork, IStockRepository stockRepository, IMapper mapper) : base(repository, unitOfWork)
		{
			_stockRepository = stockRepository;
			_mapper = mapper;
		}

		public async Task<CustomResponseDto<List<StockDto>>> GetAllWithCommentsAsync()
		{
			var stoks = await _stockRepository.GetAllWithCommentsAsync();
			var stoksDto = _mapper.Map<List<StockDto>>(stoks);
			return CustomResponseDto<List<StockDto>>.Success(200, stoksDto);
		}

		public Task<CustomResponseDto<StockDto?>> GetBySymbolAsync(string symbol)
		{
			throw new NotImplementedException();
		}

		public Task<bool> StockExist(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<CustomResponseDto<Stock>?> Update2Async(int id, UpdateStockRequestDto updateStockRequestDto)
		{
			var hasStock = await _stockRepository.GetByIdAsync(id);
			//            if (hasStock == null) return null;

			//            //hasStock.Symbol = updateStockRequestDto.Symbol;
			//            //hasStock.CompanyName = updateStockRequestDto.CompanyName;
			//            //hasStock.Purchase = updateStockRequestDto.Purchase;
			//            //hasStock.LastDiv = updateStockRequestDto.LastDiv;
			//            //hasStock.Industry = updateStockRequestDto.Industry;
			//            //hasStock.MarketCap = updateStockRequestDto.MarketCap;
			            _mapper.Map(updateStockRequestDto, hasStock);
			//            await _context.SaveChangesAsync();
			//            return hasStock;

			return CustomResponseDto<Stock>.Success(200, hasStock);


		}
	}
}
