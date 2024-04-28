using AutoMapper;
using Finance.API.Dtos.Stock;
using Finance.API.Models;

namespace Finance.API.Mappers
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Stock, StockDto>();
            CreateMap<CreateStockRequestDto, Stock>();
            CreateMap<UpdateStockRequestDto, Stock>();
        }
    }
}
