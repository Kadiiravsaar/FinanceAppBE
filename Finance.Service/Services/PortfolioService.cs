using AutoMapper;
using Finance.Core.DTOs.Portfolio;
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

		public async Task<CustomResponseDto<PortfolioDto>> CreateAsync(string symbol)
		{
			
			var portfolio = await _repo.CreateAsync(symbol);
			var portfolioDto = _mapper.Map<PortfolioDto>(portfolio);
			return CustomResponseDto<PortfolioDto>.Success(200, portfolioDto);
		}

		public async Task<CustomResponseDto<Portfolio>> DeleteAsync(string symbol)
		{
			var portfolio = await _repo.DeleteAsync(symbol);
			return CustomResponseDto<Portfolio>.Success(200, portfolio);
		}

		public async Task<CustomResponseDto<List<StockWithCommentDto>>> GetUserPortfolio()
		{
			var stocks = await _repo.GetUserPortfolio();
			var stockDto = _mapper.Map<List<StockWithCommentDto>>(stocks);
			return CustomResponseDto<List<StockWithCommentDto>>.Success(200, stockDto);
		}
	}
}
