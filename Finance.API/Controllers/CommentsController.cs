using AutoMapper;
using Finance.API.Dtos.Comment;
using Finance.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommentsController : ControllerBase
	{
		private readonly ICommentRepository _commentRepository;
		private readonly IMapper _mapper;

		public CommentsController(ICommentRepository commentRepository, IMapper mapper)
		{
			_commentRepository = commentRepository;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			var comments =  await _commentRepository.GetAllAsync();
			var map = _mapper.Map<List<CommentDto>>(comments);
			return Ok(map);	
		}
	}
}
