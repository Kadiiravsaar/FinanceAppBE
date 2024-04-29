using Finance.API.Dtos.Comment;
using Finance.API.Models;

namespace Finance.API.Interfaces
{
	public interface ICommentRepository
	{
		Task<List<Comment>> GetAllAsync();
		Task<Comment> GetByIdAsync(int id);
		Task<Comment> CreateAsync(Comment comment);
		Task<Comment> UpdateAsync(int id, UpdateCommentRequestDto updateCommentRequestDto);
		Task<Comment> DeleteAsync(int id);

	}
}
