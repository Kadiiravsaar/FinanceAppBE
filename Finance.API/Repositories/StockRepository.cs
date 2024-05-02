using AutoMapper;
using Finance.API.Data;
using Finance.API.Dtos.Comment;
using Finance.API.Dtos.Stock;
using Finance.API.Helpers;
using Finance.API.Interfaces;
using Finance.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Finance.API.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public StockRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null) return null;
                
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject queryObject)
        {
			var stocks = await _context.Stocks.AsQueryable().ToListAsync();

			if (!string.IsNullOrWhiteSpace(queryObject.CompanyName))
			{
				var companyNameLower = UserInput(queryObject.CompanyName); // büyük küçük harfe duyarlı 
				stocks = stocks.Where(x => x.CompanyName.ToLower().Contains(companyNameLower)).ToList();
			}

			if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
			{
				var symbolLower = UserInput(queryObject.Symbol);
				stocks = stocks.Where(x => x.Symbol.ToLower().Contains(symbolLower)).ToList();
			}

			return stocks;

		}

		private string UserInput(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				return input;
			}
			return input.ToLower();
		}


		/// <summary>
		/// Stoklara bağlı commentleri de çek
		/// </summary>
		/// <returns></returns>
		public async Task<List<Stock>> GetAllWithCommentsAsync()
		{
			return await _context.Stocks.Include(s => s.Comments).ToListAsync();

		}

		public async Task<Stock> GetByIdAsync(int id)
        {
            var stockId = await _context.Stocks.Include(x => x.Comments)
                .FirstOrDefaultAsync(s => s.Id == id);
            return stockId;
        }

		public Task<bool> StockExist(int id)
		{
            var hasStock = _context.Stocks.AnyAsync(x => x.Id == id);
			return hasStock;
		}

		public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStockRequestDto)
        {
            var hasStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (hasStock == null) return null;

            //hasStock.Symbol = updateStockRequestDto.Symbol;
            //hasStock.CompanyName = updateStockRequestDto.CompanyName;
            //hasStock.Purchase = updateStockRequestDto.Purchase;
            //hasStock.LastDiv = updateStockRequestDto.LastDiv;
            //hasStock.Industry = updateStockRequestDto.Industry;
            //hasStock.MarketCap = updateStockRequestDto.MarketCap;
            _mapper.Map(updateStockRequestDto, hasStock);
            await _context.SaveChangesAsync();
            return hasStock;
        }
    }
}
