using AutoMapper;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Common.Extensions;
using Delivery.Common.Models.Order;

namespace Delivery.Api.BL.MapperProfiles
{
    public class OrderMapperProfile : Profile
    {
        public OrderMapperProfile()
        {
            CreateMap<OrderEntity, OrderListModel>()
                .ForMember(dst => dst.TotalPrice, expr => expr.MapFrom(src => src.DishAmounts.Sum(x => x.Dish!.Price * x.Amount)));
            CreateMap<OrderEntity, OrderDetailModel>()
                .ForMember(dst => dst.TotalPrice, expr => expr.MapFrom(src => src.DishAmounts.Sum(x => x.Dish!.Price * x.Amount)));

            CreateMap<OrderCreateModel, OrderEntity>()
            .Ignore(dst => dst.Restaurant);
        }
    }
}
