using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Core.Models
{
	public class Comment : BaseEntity
	{
		public int? StockId { get; set; }
		public string? AppUserId { get; set; }

		public string Title { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;

		public Stock? Stock { get; set; }
		public AppUser AppUser { get; set; }
	}
}
