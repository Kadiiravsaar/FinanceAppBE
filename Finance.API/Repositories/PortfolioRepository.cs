using Finance.API.Data;
using Finance.API.Extensions;
using Finance.API.Interfaces;
using Finance.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Finance.API.Repositories
{
	public class PortfolioRepository : IPortfolioRepository
	{
		private readonly AppDbContext _context;
		private readonly UserManager<AppUser> _userManager;
		private readonly IStockRepository _stockRepository;
		//private readonly IFMPService _fmpService;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public PortfolioRepository(AppDbContext context,
			UserManager<AppUser> userManager,
			IStockRepository stockRepository,
			IHttpContextAccessor httpContextAccessor
			//IFMPService fmpService
			)
		{
			_context = context;
			_userManager = userManager;
			_stockRepository = stockRepository;
			_httpContextAccessor = httpContextAccessor;
			//_fmpService = fmpService;
		}

		public async Task<Portfolio> CreateAsync(string symbol)
		{
			// Kullanıcının ID'sini al ve kullanıcı bul

			var userId = await GetCurrentUserAsync();


			// Hisse senedini bul
			var hasStock = await _stockRepository.GetBySymbolAsync(symbol);
			if (hasStock == null) return null;


			// Kullanıcının portföyünü kontrol et
			var getUser = await GetUserPortfolio();

			if (getUser.Any(s => s.Symbol.ToLower() == symbol.ToLower())) return null; // sembolü var mı kontrol eder ve varsa küçük harfe çevirir

			// Yeni portföy modelini oluştur
			var newPortföy = new Portfolio
			{

				AppUserId = userId.Id,
				StockId = hasStock.Id
			};

			await _context.AddAsync(newPortföy);
			await _context.SaveChangesAsync();
			return newPortföy;

		}

		private async Task<AppUser> GetCurrentUserAsync()
		{
			var userId = _httpContextAccessor.HttpContext.User.GetUserId();
			var appUser = await _userManager.FindByIdAsync(userId);
			return appUser;
		}

		public async Task<List<Stock>> GetUserPortfolio()
		{
			var user = await GetCurrentUserAsync();

			var stocks = await _context.Portfolios
		   .Where(p => p.AppUserId == user.Id)
		   .Select(stock => new Stock
		   {
			   Id = stock.StockId,
			   Symbol = stock.Stock.Symbol,
			   CompanyName = stock.Stock.CompanyName,
			   Purchase = stock.Stock.Purchase,
			   LastDiv = stock.Stock.LastDiv,
			   Industry = stock.Stock.Industry,
			   MarketCap = stock.Stock.MarketCap
		   }).ToListAsync();

			return stocks;
		}
	}
}
