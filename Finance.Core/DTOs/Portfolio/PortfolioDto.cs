using Finance.Core.DTOs.Stock;
using Finance.Core.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Core.DTOs.Portfolio
{
	public class PortfolioDto
	{
		public UserDto AppUser { get; set; }
		public StockDto Stock { get; set; }
	}
}
