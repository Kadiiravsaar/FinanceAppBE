using AutoMapper;
using Finance.API.Dtos.Comment;
using Finance.API.Dtos.Stock;
using Finance.API.Dtos.User;
using Finance.API.Models;

namespace Finance.API.Mappers
{
    public class MapProfile : Profile
    {	
        public MapProfile()
		{
			CreateMap<Stock, StockDto>();
			CreateMap<FMPStock, Stock>();
			CreateMap<StockDto, List<StockCommentDto>>();
			CreateMap<Comment, StockCommentDto>();
			CreateMap<CreateStockRequestDto, Stock>();
			CreateMap<UpdateStockRequestDto, Stock>();
			CreateMap<CreateCommentRequestDto, CommentDto>();
				

			CreateMap<AppUser, UserDto>();

			CreateMap<CreateCommentRequestDto, Comment>();
			CreateMap<Comment, CommentDto>()
				.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.AppUser.UserName)); // Yeni alanın eşlenmesi
			
				
		}
    }
}
