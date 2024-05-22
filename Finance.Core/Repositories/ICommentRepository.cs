using Finance.Core.DTOs.Comment;
using Finance.Core.DTOs.Result;
using Finance.Core.Models;

namespace Finance.Core.Repositories
{
	public interface ICommentRepository : IGenericRepository<Comment>
	{
		Task<Comment> CreateAsync(Comment comment);

		Task<List<Comment>> GetAllWithUserAsync();

		Task<Comment> GetByIdAsyncWithUser(int id);

	}
}
