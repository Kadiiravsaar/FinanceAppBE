using System.ComponentModel.DataAnnotations;

namespace Finance.Core.DTOs.Stock
{
    public class CreateStockRequestDto
    {
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
        private DateTime _createdOn = DateTime.UtcNow;
        public DateTime CreatedOn
        {
            get { return _createdOn; }
            private set { _createdOn = value; }
        }
    }
}
