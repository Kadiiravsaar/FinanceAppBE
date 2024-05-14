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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Service.Services
{
	public class CommentService : Service<Comment>, ICommentService
	{
		private readonly IStockRepository _stockRepository;
		private readonly ICommentRepository _commentRepository;
		private readonly IFMPService _fmpService;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<AppUser> _userManager;
		public CommentService(IGenericRepository<Comment> repository, IUnitOfWork unitOfWork, IStockRepository stockRepository, IMapper mapper, IFMPService fmpService, ICommentRepository commentRepository, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager) : base(repository, unitOfWork)
		{
			_stockRepository = stockRepository;
			_mapper = mapper;
			_fmpService = fmpService;
			_commentRepository = commentRepository;
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
		}

		public async Task<CustomResponseDto<CommentDto>> CreateAsync(string symbol, CreateCommentRequestDto createCommentRequestDto)
		{
			var appUser = await GetCurrentUserAsync();
			var stock = await GetStockBySymbolAsync(symbol);

			if (stock == null) throw new Exception("Stock does not exists");

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
		private async Task<AppUser> GetCurrentUserAsync()
		{
			var userId = _httpContextAccessor.HttpContext.User.GetUserId();
			return await _userManager.FindByIdAsync(userId);
		}

		private async Task<StockCommentDto> GetStockBySymbolAsync(string symbol)
		{
			var stock = await _stockRepository.GetBySymbolAsync(symbol);
			if (stock == null)
			{
				stock = await _fmpService.FindStockBySymbolAsync(symbol);
				if (stock != null)
				{
					await _stockRepository.AddAsync(stock);
				}
			}
			var map = _mapper.Map<StockCommentDto>(stock);
			return map;
		}
	}
}
