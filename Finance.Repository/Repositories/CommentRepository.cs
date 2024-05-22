using AutoMapper;
using Finance.Core.Extensions;
using Finance.Core.Models;
using Finance.Core.Repositories;
using Finance.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Repository.Repositories
{
	public class CommentRepository : GenericRepository<Comment>, ICommentRepository
	{

		private readonly IStockRepository _stockRepository;

		public CommentRepository(AppDbContext context, IStockRepository stockRepository) : base(context)
		{
			_stockRepository = stockRepository;
	
		}

		public async Task<Comment> CreateAsync(Comment comment)
		{
			await _context.AddAsync(comment);
			await _context.SaveChangesAsync();
			return comment;
		}

		public async Task<List<Comment>> GetAllWithUserAsync()
		{

			return await _context.Comments.Include(c => c.AppUser).Include(c=>c.Stock).ToListAsync();
			
		}

		public async Task<Comment> GetByIdAsyncWithUser(int id)
		{
			return await _context.Comments.Include(c => c.AppUser).FirstOrDefaultAsync(c => c.Id == id);
		}
	}
}
