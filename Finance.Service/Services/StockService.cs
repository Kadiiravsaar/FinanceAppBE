using AutoMapper;
using Finance.Core.DTOs.Result;
using Finance.Core.DTOs.Stock;
using Finance.Core.Models;
using Finance.Core.Repositories;
using Finance.Core.Services;
using Finance.Core.UnitOfWorks;

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

	}
}
