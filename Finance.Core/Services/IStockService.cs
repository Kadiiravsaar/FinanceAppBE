using Finance.Core.DTOs.Result;
using Finance.Core.DTOs.Stock;
using Finance.Core.Models;

namespace Finance.Core.Services
{
    public interface IStockService : IService<Stock>
	{
		public Task<CustomResponseDto<List<StockDto>>> GetAllWithCommentsAsync();

		Task<CustomResponseDto<StockDto?>> GetBySymbolAsync(string symbol);
		Task<CustomResponseDto<StockDto>> GetOrAddStockAsync(string symbol);

		Task<CustomResponseDto<List<StockDto>>> GetStocksAsync(QueryObject queryObject);
	}
}


