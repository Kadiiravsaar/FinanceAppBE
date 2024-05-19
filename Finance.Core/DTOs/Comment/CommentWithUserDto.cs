namespace Finance.Core.DTOs.Comment
{
	public class CommentWithUserDto
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
		public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
		public string CreatedBy { get; set; } = string.Empty;
		public int? StockId { get; set; }
		public string? Symbol { get; set; }
	}
}
