using AutoMapper;
using Finance.API.Data;
using Finance.API.Dtos.Comment;
using Finance.API.Dtos.Stock;
using Finance.API.Helpers;
using Finance.API.Interfaces;
using Finance.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

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

		public async Task<Stock?> GetBySymbolAsync(string symbol)
		{
			return await _context.Stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);
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

			if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
			{
				var param = Expression.Parameter(typeof(Stock), "s");
				var sortExpression = Expression.Lambda<Func<Stock, object>>(Expression.Convert(Expression.Property(param, queryObject.SortBy), typeof(object)), param);
				stocks = queryObject.IsDecsending ? stocks.AsQueryable().OrderByDescending(sortExpression).ToList() : stocks.AsQueryable().OrderBy(sortExpression).ToList();
			}

            //stocks = stocks.Skip((queryObject.PageNumber - 1) * queryObject.PageSize).Take(queryObject.PageSize).ToList();

            var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;
			//Bu satırda, atlanacak stok sayısını hesaplıyoruz.queryObject.PageNumber ve queryObject.PageSize genellikle bir web sayfasında gösterilecek veri miktarını kontrol etmek için kullanılır.
			//PageNumber: Kullanıcının hangi sayfada olduğunu belirtir.Örneğin, eğer kullanıcı 3.sayfadaysa, PageNumber 3 olacaktır.
			//PageSize: Her sayfada kaç adet stok gösterileceğini belirtir.Örneğin, eğer her sayfada 10 stok göstermek istiyorsak, PageSize 10 olacaktır.
			//Bu iki değeri kullanarak, atlanacak stok sayısını(skipNumber) hesaplıyoruz.Eğer kullanıcı 3.sayfadaysa ve her sayfada 10 stok gösteriliyorsa, ilk 20 stok atlanacak ve 21.stoktan itibaren stoklar gösterilecektir.

			return stocks.Skip(skipNumber).Take(queryObject.PageSize).ToList();
			//Bu satırda, öncelikle Skip(skipNumber) metodu ile belirli sayıda stok atlanır. Sonra Take(queryObject.PageSize) metodu ile belirli sayıda stok alınır. Son olarak, ToList() metodu ile bu stoklar bir liste haline getirilir ve döndürülür.


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
