using Finance.Core.DTOs.Comment;
using Finance.Core.DTOs.Result;
using Finance.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Finance.Core.Services
{
	public interface ICommentService : IService<Comment>
	{
		Task<CustomResponseDto<CommentWithUserDto>> CreateAsync(string symbol,CreateCommentRequestDto createCommentRequestDto);
		Task<CustomResponseDto<List<CommentWithUserDto>>> GetAllWithUserAsync();

		Task<CustomResponseDto<CommentWithUserDto>> GetByIdWithUser(int id);



	

	}

}
