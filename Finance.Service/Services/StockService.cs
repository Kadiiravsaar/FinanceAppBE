using AutoMapper;
using Finance.Core.DTOs.Result;
using Finance.Core.DTOs.Stock;
using Finance.Core.Models;
using Finance.Core.Repositories;
using Finance.Core.Services;
using Finance.Core.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Finance.Service.Services
{
    public class StockService : Service<Stock>, IStockService
	{

		private readonly IStockRepository _stockRepository;
		private readonly IMapper _mapper;
		private readonly IFMPService _fmpService;

		public StockService(IGenericRepository<Stock> repository, IUnitOfWork unitOfWork, IStockRepository stockRepository, IMapper mapper, IFMPService fmpService) : base(repository, unitOfWork)
		{
			_stockRepository = stockRepository;
			_mapper = mapper;
			_fmpService = fmpService;
		}

		public async Task<CustomResponseDto<List<StockDto>>> GetAllWithCommentsAsync()
		{
			var stoks = await _stockRepository.GetAllWithCommentsAsync();
			var stoksDto = _mapper.Map<List<StockDto>>(stoks);
			return CustomResponseDto<List<StockDto>>.Success(200, stoksDto);
		}
		public async Task<CustomResponseDto<List<StockDto>>> GetStocksAsync(QueryObject queryObject)
		{
			var query = _stockRepository.GetAll();

			if (!string.IsNullOrWhiteSpace(queryObject.CompanyName))
			{
				var companyNameLower = queryObject.CompanyName.ToLower();
				query = query.Where(x => x.CompanyName.ToLower().Contains(companyNameLower));
			}

			if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
			{
				var symbolLower = queryObject.Symbol.ToLower();
				query = query.Where(x => x.Symbol.ToLower().Contains(symbolLower));
			}

			if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
			{
				var param = Expression.Parameter(typeof(Stock), "s");
				var sortExpression = Expression.Lambda<Func<Stock, object>>(Expression.Convert(Expression.Property(param, queryObject.SortBy), typeof(object)), param);
				query = queryObject.IsDecsending ? query.OrderByDescending(sortExpression) : query.OrderBy(sortExpression);
			}

			var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;
			query = query.Skip(skipNumber).Take(queryObject.PageSize);

			var stocks = await query.ToListAsync();
			var stocksDto = _mapper.Map<List<StockDto>>(stocks);
			return CustomResponseDto<List<StockDto>>.Success(200, stocksDto);
		}

		public async Task<CustomResponseDto<StockDto?>> GetBySymbolAsync(string symbol)
		{
			var stoks = await _stockRepository.GetBySymbolAsync(symbol);
			var stoksDto = _mapper.Map<StockDto>(stoks);
			return CustomResponseDto<StockDto>.Success(200, stoksDto);
		}

		public async Task<CustomResponseDto<StockDto>> GetOrAddStockAsync(string symbol)
		{
			var stock = await _stockRepository.GetBySymbolAsync(symbol);
			if (stock == null)
			{
				stock = await _fmpService.FindStockBySymbolAsync(symbol);
				if (stock != null)
				{
					await _stockRepository.AddStock(stock);
				}
			}
			var stoksDto = _mapper.Map<StockDto>(stock);
			return CustomResponseDto<StockDto>.Success(200, stoksDto);
		}

	}
}
