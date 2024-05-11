using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Core.Models
{
	public class Stock : BaseEntity
	{
		public string Symbol { get; set; } = string.Empty;
		public string CompanyName { get; set; } = string.Empty;

		[Column(TypeName = "decimal(18,2)")]
		public decimal Purchase { get; set; } // satın alma fiyatı

		[Column(TypeName = "decimal(18,2)")]
		public decimal LastDiv { get; set; }
		public string Industry { get; set; } = string.Empty;
		public long MarketCap { get; set; }

		public List<Comment> Comments { get; set; } = new List<Comment> { };
		public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();


	}
}
