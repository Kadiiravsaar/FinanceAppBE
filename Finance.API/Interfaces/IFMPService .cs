using Finance.API.Models;

namespace Finance.API.Interfaces
{
	public interface IFMPService
	{
		Task<Stock> FindStockBySymbolAsync(string symbol);
	}
}
