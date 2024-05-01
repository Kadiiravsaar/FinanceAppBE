﻿namespace Finance.API.Dtos.Comment
{
	public class CreateCommentRequestDto
	{
		public string Title { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
		public int? StockId { get; set; }

	}
}
