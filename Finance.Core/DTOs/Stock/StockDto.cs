using Finance.Core.DTOs.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Core.DTOs.Stock
{
	public class StockDto
	{
		public int Id { get; set; }
		public string Symbol { get; set; } = string.Empty;
		public string CompanyName { get; set; } = string.Empty;
		public decimal Purchase { get; set; } // satın alma fiyatı
		public decimal LastDiv { get; set; }
		public string Industry { get; set; } = string.Empty;
		public long MarketCap { get; set; }
		public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
	}
}
