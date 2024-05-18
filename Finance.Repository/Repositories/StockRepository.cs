using Finance.Core.DTOs.Stock;
using Finance.Core.Models;
using Finance.Core.Repositories;
using Finance.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace Finance.Repository.Repositories
{
	public class StockRepository : GenericRepository<Stock>, IStockRepository
	{
		public StockRepository(AppDbContext context) : base(context)
		{
		}

		public async Task<List<Stock>> GetAllWithCommentsAsync()
		{
			return await _context.Stocks.Include(s => s.Comments).ThenInclude(c => c.AppUser).ToListAsync();

		}

		public async Task<Stock?> GetBySymbolAsync(string symbol)
		{
			return await _context.Stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);			 
		}

		public Task<bool> StockExist(int id)
		{
			var hasStock = _context.Stocks.AnyAsync(x => x.Id == id);
			return hasStock;
		}

		public async Task AddStock(Stock stock)
		{
			_context.Stocks.Add(stock);
			await _context.SaveChangesAsync();
		}
	}




}
