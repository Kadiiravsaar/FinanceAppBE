using AutoMapper;
using Finance.Core.DTOs.Comment;
using Finance.Core.DTOs.Result;
using Finance.Core.DTOs.Stock;
using Finance.Core.DTOs.User;
using Finance.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
			// Stock Mapping

			CreateMap<Stock, StockDto>();
            CreateMap<Stock, StockCommentDto>();
            CreateMap<FMPStock, Stock>();
            CreateMap<CreateStockRequestDto, Stock>().ReverseMap();
            CreateMap<UpdateStockRequestDto, Stock>().ReverseMap();


            // AppUser mappinh
			CreateMap<AppUser, UserDto>();



			// Comment Mapping
			CreateMap<CreateCommentRequestDto, CommentWithUserDto>();
            CreateMap<UpdateCommentRequestDto, Comment>();
			CreateMap<CreateCommentRequestDto, Comment>();
            CreateMap<Comment, CommentDto>();

            CreateMap<Comment, CommentWithUserDto>()
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.AppUser.UserName))// Yeni alanın eşlenmesi
                .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Stock.Symbol));

			CreateMap<Comment, StockCommentDto>()
			   .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.AppUser.UserName));
		}
    }
}
