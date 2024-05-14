using Finance.Core.DTOs.Comment;
using Finance.Core.DTOs.Result;
using Finance.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Finance.Core.Services
{
	public interface ICommentService : IService<Comment>
	{
		Task<CustomResponseDto<CommentDto>> CreateAsync(string symbol,CreateCommentRequestDto createCommentRequestDto);
		Task<CustomResponseDto<List<CommentDto>>> GetAllWithUserAsync();


		//public async Task<List<Comment>> GetAllAsync()
		//{
		//	return await _context.Comments.Include(c => c.AppUser).ToListAsync();
		//}

	}

}
