using Finance.Core.DTOs.Portfolio;
using Finance.Core.DTOs.Result;
using Finance.Core.DTOs.Stock;
using Finance.Core.Models;

namespace Finance.Core.Services
{
    public interface IPortfolioService : IService<Portfolio>
	{
		public Task<CustomResponseDto<List<StockWithCommentDto>>> GetUserPortfolio();
		Task<CustomResponseDto<PortfolioDto>> CreateAsync(string symbol);
		Task<CustomResponseDto<Portfolio>> DeleteAsync(string symbol);
	}
	

}
