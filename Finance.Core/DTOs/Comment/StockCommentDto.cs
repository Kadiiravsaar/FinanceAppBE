namespace Finance.Core.DTOs.Comment
{
	public class StockCommentDto
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;

		public string CreatedBy { get; set; } = string.Empty;
		public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
	}
}
