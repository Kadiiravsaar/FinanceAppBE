using AutoMapper;
using Finance.API.Dtos.Comment;
using Finance.API.Interfaces;
using Finance.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommentsController : ControllerBase
	{
		private readonly ICommentRepository _commentRepository;
		private readonly IStockRepository _stockRepository;
		private readonly IMapper _mapper;

		public CommentsController(ICommentRepository commentRepository, IMapper mapper, IStockRepository stockRepository)
		{
			_commentRepository = commentRepository;
			_mapper = mapper;
			_stockRepository = stockRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			var comments =  await _commentRepository.GetAllAsync();
			var map = _mapper.Map<List<CommentDto>>(comments);
			return Ok(map);	
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var comment = await _commentRepository.GetByIdAsync(id);
			var map = _mapper.Map<CommentDto>(comment);
			if (map == null) return NotFound();
			return Ok(map);
		}

		[HttpPost("{stockId}")]
		public async Task<IActionResult> CreateComment([FromRoute] int stockId, CreateCommentRequestDto createCommentRequestDto)
		{
			var stock = await _stockRepository.StockExist(stockId);
			if (stockId != createCommentRequestDto.StockId)
			{
				return BadRequest("Stok ID'leri eşleşmiyor");
			}
			if (!stock) 
			{
				return BadRequest("Stok bulunmamakta");
			}

			var commentModel = _mapper.Map<Comment>(createCommentRequestDto);
			var createdComment =  await _commentRepository.CreateAsync(commentModel);
			var commentDto = _mapper.Map<CommentDto>(createdComment); // Bu satırı ekleyin

			return Ok(commentDto); // Bu satırı düzeltin
		}


		[HttpDelete]
        [Route("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			await _commentRepository.DeleteAsync(id);
			return NoContent();
		}
	}
}
