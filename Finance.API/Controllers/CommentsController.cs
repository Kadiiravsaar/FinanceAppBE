using AutoMapper;

using Finance.Core.DTOs.Comment;
using Finance.Core.DTOs.Result;
using Finance.Core.DTOs.Stock;
using Finance.Core.Models;
using Finance.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommentsController : ControllerBase
	{
		private readonly ICommentService _commentService;
		private readonly IMapper _mapper;

		public CommentsController(IMapper mapper, ICommentService commentService)
		{
			_mapper = mapper;
			_commentService = commentService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			var comments = await _commentService.GetAllAsync();
			var commentDto = _mapper.Map<List<CommentDto>>(comments);
			return Ok(CustomResponseDto<List<CommentDto>>.Success(200, commentDto));
		}

		[HttpGet("GetAllWithUserAsync")]
		public async Task<IActionResult> GetAllWithUserAsync()
		{
			var comments = await _commentService.GetAllWithUserAsync();
			return Ok(CustomResponseDto<List<CommentWithUserDto>>.Success(200, comments.Data));
		}


		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var comment = await _commentService.GetByIdAsync(id);
			var commentDto = _mapper.Map<CommentDto>(comment);
			return Ok(CustomResponseDto<CommentDto>.Success(200, commentDto));
		}

		[HttpGet("GetByIdAsyncWithUser")]
		public async Task<IActionResult> GetByIdAsyncWithUser(int id)
		{
			var comment = await _commentService.GetByIdWithUser(id);
			if (comment.StatusCode == 404)
			{
				return NotFound(CustomResponseDto<CommentDto>.Fail(404, comment.Errors));
			}
			var commentDto = _mapper.Map<CommentDto>(comment.Data);
			return Ok(CustomResponseDto<CommentDto>.Success(200, commentDto));
		}


		[Authorize]
		[HttpPost("{symbol:alpha}")]
		public async Task<IActionResult> CreateComment([FromRoute] string symbol, CreateCommentRequestDto createCommentRequestDto)
		{
			var comment = await _commentService.CreateAsync(symbol, createCommentRequestDto);
			return Ok(CustomResponseDto<CommentWithUserDto>.Success(200, comment.Data));
		}
		[HttpPut]
		[Route("{id:int}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
		{
			var comment = await _commentService.GetByIdAsync(id);
			await _commentService.UpdateAsync(_mapper.Map(updateDto, comment));
			return Ok(CustomResponseDto<NoContentDto>.Success(200));

		}

		[HttpDelete]
		[Route("{id:int}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var comment = await _commentService.GetByIdAsync(id);
			await _commentService.RemoveAsync(comment);
			return Ok(CustomResponseDto<NoContentDto>.Success(200));
		}
	}
}
