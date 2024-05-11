using Finance.Core.Models;

namespace Finance.Core.Repositories
{
	public interface IPortfolioRepository : IGenericRepository<Portfolio>
	{
		Task<List<Stock>> GetUserPortfolio();
		Task<Portfolio> CreateAsync(string symbol);
		Task<Portfolio> DeleteAsync(string symbol);
	}


	
}
