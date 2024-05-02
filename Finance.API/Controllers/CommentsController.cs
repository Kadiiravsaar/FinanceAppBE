using AutoMapper;
using Finance.API.Dtos.Comment;
using Finance.API.Dtos.Stock;
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
			if (!ModelState.IsValid) return BadRequest(ModelState);
			
			var comments =  await _commentRepository.GetAllAsync();
			var map = _mapper.Map<List<CommentDto>>(comments);
			return Ok(map);	
		}


		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var comment = await _commentRepository.GetByIdAsync(id);
			var map = _mapper.Map<CommentDto>(comment);
			if (map == null) return NotFound();
			return Ok(map);
		}

		[HttpPost("{stockId:int}")]
		public async Task<IActionResult> CreateComment([FromRoute] int stockId, CreateCommentRequestDto createCommentRequestDto)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

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
		[HttpPut]
		[Route("{id:int}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var commentModel = await _commentRepository.UpdateAsync(id, updateDto);
			if (commentModel == null) return NotFound();
			return Ok(updateDto);
		}

		[HttpDelete]
        [Route("{id:int}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			await _commentRepository.DeleteAsync(id);
			return NoContent();
		}
	}
}
