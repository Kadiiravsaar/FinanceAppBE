using AutoMapper;
using Finance.Core.DTOs.Comment;
using Finance.Core.DTOs.Result;
using Finance.Core.DTOs.Stock;
using Finance.Core.Extensions;
using Finance.Core.Models;
using Finance.Core.Repositories;
using Finance.Core.Services;
using Finance.Core.UnitOfWorks;
using Finance.Repository.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Service.Services
{
	public class CommentService : Service<Comment>, ICommentService
	{
		private readonly IStockService _stockService ;
		private readonly ICommentRepository _commentRepository;

		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<AppUser> _userManager;
		public CommentService(IGenericRepository<Comment> repository, IUnitOfWork unitOfWork, IMapper mapper, ICommentRepository commentRepository, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IStockService stockService) : base(repository, unitOfWork)
		{
			_mapper = mapper;
			_commentRepository = commentRepository;
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
			_stockService = stockService;
		}

		public async Task<CustomResponseDto<CommentDto>> CreateAsync(string symbol, CreateCommentRequestDto createCommentRequestDto)
		{
			var appUser = await GetCurrentUserAsync();

			var stock = await _stockService.GetOrAddStockAsync(symbol);
			if (stock == null) throw new Exception("Stock does not exist");

			var commentModel = _mapper.Map<Comment>(createCommentRequestDto);

			commentModel.StockId = stock.Id;
			commentModel.AppUserId = appUser.Id;

			await _commentRepository.CreateAsync(symbol,commentModel);
			var commentDto = _mapper.Map<CommentDto>(commentModel);

			return CustomResponseDto<CommentDto>.Success(200,commentDto);
		}

		public async Task<CustomResponseDto<List<CommentDto>>> GetAllWithUserAsync()
		{

			var commentWithUser = await _commentRepository.GetAllWithUserAsync();
			var commentWithUserDto = _mapper.Map<List<CommentDto>>(commentWithUser);
			return CustomResponseDto<List<CommentDto>>.Success(200, commentWithUserDto);


		}

		public async Task<CustomResponseDto<CommentDto>> GetByIdWithUser(int id)
		{
			var comment = await _commentRepository.GetByIdAsyncWithUser(id);
			if (comment == null)
			{
				return CustomResponseDto<CommentDto>.Fail(404, "Girilen ID'ye sahip bir yorum yok");
			}
			var commentDto = _mapper.Map<CommentDto>(comment);
			return CustomResponseDto<CommentDto>.Success(200, commentDto);
		}

		private async Task<AppUser> GetCurrentUserAsync()
		{
			var userId = _httpContextAccessor.HttpContext.User.GetUserId();
			return await _userManager.FindByIdAsync(userId);
		}

		
	}
}
