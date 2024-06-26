﻿using Finance.Core.Extensions;
using Finance.Core.Models;
using Finance.Core.Repositories;
using Finance.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Finance.Repository.Repositories
{
	public class PortfolioRepository : GenericRepository<Portfolio>, IPortfolioRepository
	{

		private readonly UserManager<AppUser> _userManager;
		private readonly IStockRepository _stockRepository;
		private readonly IFMPService _fmpService;
		private readonly IHttpContextAccessor _httpContextAccessor;



		public PortfolioRepository(AppDbContext context, UserManager<AppUser> userManager, IStockRepository stockRepository, IHttpContextAccessor httpContextAccessor, IFMPService fmpService) : base(context)
		{
			_userManager = userManager;
			_stockRepository = stockRepository;
			_httpContextAccessor = httpContextAccessor;
			_fmpService = fmpService;
		}

		public async Task<Portfolio> CreateAsync(string symbol)
		{
			// Kullanıcının ID'sini al ve kullanıcı bul

			var userId = await GetCurrentUserAsync();


			// Hisse senedini bul
			var hasStock = await _stockRepository.GetBySymbolAsync(symbol);
			if (hasStock == null)
			{
				hasStock = await _fmpService.FindStockBySymbolAsync(symbol);
				if (hasStock == null)
				{
					return null;
				}
				else
				{
					await _stockRepository.AddStock(hasStock);
				}
			}



			// Kullanıcının portföyünü kontrol et
			var getUser = await GetUserPortfolio();

			if (getUser.Any(s => s.Symbol.ToLower() == symbol.ToLower())) return null; // sembolü var mı kontrol eder ve varsa küçük harfe çevirir

			// Yeni portföy modelini oluştur
			var newPortfolio = new Portfolio
			{

				AppUserId = userId.Id,
				StockId = hasStock.Id
			};

			await _context.AddAsync(newPortfolio);
			await _context.SaveChangesAsync();
			return newPortfolio;

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
			//var userId = _httpContextAccessor.HttpContext.User.GetUserId();
			//var appUserr = await _userManager.FindByIdAsync(userId);

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

		public async Task<Portfolio> DeleteAsync(string symbol)
		{

			var currenUser = await GetCurrentUserAsync();
			var userPortfolio = await GetUserPortfolio();


			var portfolioToDelete = await _context.Portfolios
				.Include(p => p.Stock)  // Stock nesnesini yükle
				.FirstOrDefaultAsync(x => x.AppUserId == currenUser.Id && x.Stock.Symbol == symbol);

			if (portfolioToDelete == null) return null;

			_context.Portfolios.Remove(portfolioToDelete);
			await _context.SaveChangesAsync();

			return portfolioToDelete;
		}
	}
}






