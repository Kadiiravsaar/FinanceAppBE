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
            CreateMap<Stock, StockDto>();
            CreateMap<Stock, StockCommentDto>();
            CreateMap<FMPStock, Stock>();

            CreateMap<Comment, StockCommentDto>();
            CreateMap<CreateStockRequestDto, Stock>().ReverseMap();
            CreateMap<UpdateStockRequestDto, Stock>().ReverseMap();
            CreateMap<CreateCommentRequestDto, CommentDto>();
            CreateMap<UpdateCommentRequestDto, Comment>();

			



			CreateMap<AppUser, UserDto>();

            CreateMap<CreateCommentRequestDto, Comment>();
            CreateMap<Comment, CommentDto>()
            	.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.AppUser.UserName)); // Yeni alanın eşlenmesi


        }
    }
}
