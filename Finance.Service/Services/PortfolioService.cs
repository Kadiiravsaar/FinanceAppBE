using AutoMapper;
using Finance.Core.DTOs.Result;
using Finance.Core.DTOs.Stock;
using Finance.Core.Models;
using Finance.Core.Repositories;
using Finance.Core.Services;
using Finance.Core.UnitOfWorks;


namespace Finance.Service.Services
{
	public class PortfolioService : Service<Portfolio>, IPortfolioService
	{
		private readonly IPortfolioRepository _repo;
		private readonly IMapper _mapper;
		public PortfolioService(IGenericRepository<Portfolio> repository, IUnitOfWork unitOfWork, IPortfolioRepository repo , IMapper mapper) : base(repository, unitOfWork)
		{
			_repo = repo;
			_mapper = mapper;
		}

		public async Task<CustomResponseDto<Portfolio>> CreateAsync(string symbol)
		{
			var portfolio = await _repo.CreateAsync(symbol);
			return CustomResponseDto<Portfolio>.Success(200, portfolio);
		}

		public async Task<CustomResponseDto<Portfolio>> DeleteAsync(string symbol)
		{
			var portfolio = await _repo.DeleteAsync(symbol);
			return CustomResponseDto<Portfolio>.Success(200, portfolio);
		}

		public async Task<CustomResponseDto<List<StockDto>>> GetUserPortfolio()
		{
			var stocks = await _repo.GetUserPortfolio();
			var stockDto = _mapper.Map<List<StockDto>>(stocks);
			return CustomResponseDto<List<StockDto>>.Success(200, stockDto);
		}
	}
}
