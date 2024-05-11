using Finance.Core.DTOs;
using Finance.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Core.Repositories
{
	public interface IStockRepository: IGenericRepository<Stock>
	{
		Task<List<Stock>> GetAllWithCommentsAsync();
		Task<Stock?> GetBySymbolAsync(string symbol);
		
		Task<bool> StockExist(int id);


	}



}
