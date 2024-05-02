using Finance.API.Dtos.Comment;
using Finance.API.Dtos.Stock;
using Finance.API.Helpers;
using Finance.API.Models;

namespace Finance.API.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject queryObject);
        Task<Stock> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStockRequestDto);
        Task<Stock?> DeleteAsync(int id);
		Task<List<Stock>> GetAllWithCommentsAsync();

        Task<bool> StockExist(int id);


	}
}
