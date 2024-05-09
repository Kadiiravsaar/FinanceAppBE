using AutoMapper;
using Finance.API.Data;
using Finance.API.Dtos.Comment;
using Finance.API.Dtos.Stock;
using Finance.API.Extensions;
using Finance.API.Interfaces;
using Finance.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Finance.API.Repositories
{
	public class CommentRepository : ICommentRepository
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<AppUser> _userManager;




		public CommentRepository(AppDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
		{
			_context = context;
			_mapper = mapper;
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
		}

		public async Task<Comment> CreateAsync(Comment comment)
		{
			var userId = _httpContextAccessor.HttpContext.User.GetUserId();
			var appUser = await _userManager.FindByIdAsync(userId);
			comment.AppUserId = appUser.Id;
			//comment.AppUser.UserName = appUser.UserName;
			await _context.Comments.AddAsync(comment);
			await _context.SaveChangesAsync();
			return comment;
		}

		public async Task<Comment> DeleteAsync(int id)
		{
			var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
			if (commentModel == null) return null;
			_context.Remove(commentModel);
			await _context.SaveChangesAsync();
			return commentModel;
		}

		public async Task<List<Comment>> GetAllAsync()
		{
			return await _context.Comments.Include(c=>c.AppUser).ToListAsync();
		}

		public async Task<Comment> GetByIdAsync(int id)
		{
			var commentId = await _context.Comments.Include(c => c.AppUser).FirstOrDefaultAsync(c=>c.Id == id);
			return commentId;

		}

		public async Task<Comment> UpdateAsync(int id, UpdateCommentRequestDto updateCommentRequestDto)
		{
			var hasComment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
			if (hasComment == null) return null;
			_mapper.Map(updateCommentRequestDto, hasComment);
			await _context.SaveChangesAsync();
			return hasComment;
		}
	}
}
