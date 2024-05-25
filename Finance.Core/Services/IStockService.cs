using Finance.Core.DTOs.Result;
using Finance.Core.DTOs.Stock;
using Finance.Core.Models;

namespace Finance.Core.Services
{
    public interface IStockService : IService<Stock>
	{
		public Task<CustomResponseDto<List<StockWithCommentDto>>> GetAllWithCommentsAsync();

		Task<CustomResponseDto<StockWithCommentDto?>> GetBySymbolAsync(string symbol);
		Task<CustomResponseDto<StockWithCommentDto>> GetOrAddStockAsync(string symbol);

		Task<CustomResponseDto<List<StockWithCommentDto>>> GetStocksAsync(QueryObject queryObject);
	}
}


