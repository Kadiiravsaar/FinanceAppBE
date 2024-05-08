using Finance.API.Models;

namespace Finance.API.Interfaces
{
	public interface IPortfolioRepository
	{
		Task<List<Stock>> GetUserPortfolio();
		Task<Portfolio> CreateAsync(string symbol);
		Task<Portfolio> DeleteAsync(string symbol);
	}
}
