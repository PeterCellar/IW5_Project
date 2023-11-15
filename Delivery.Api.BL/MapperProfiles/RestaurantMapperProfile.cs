using AutoMapper;
using Delivery.Api.BL.Facades;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Common.Enums;
using Delivery.Common.Extensions;
using Delivery.Common.Models;
using Delivery.Common.Models.Restaurant;

namespace Delivery.Api.BL.MapperProfiles
{
    public class RestaurantMapperProfile : Profile
    {
        public RestaurantMapperProfile()
        {
            CreateMap<RestaurantEntity, RestaurantListModel>()
                .ForMember(dst => dst.Revenue, expr => expr.MapFrom(src => src.Orders.Sum(x => x.DishAmounts.Sum(y => y.Dish!.Price * y.Amount))));

            CreateMap<RestaurantEntity, RestaurantDetailModel>()
                .ForMember(dst => dst.Revenue, expr => expr.MapFrom(src => src.Orders.Sum(x => x.State == OrderState.Delivered ? x.DishAmounts.Sum(y => y.Dish!.Price * y.Amount) : Convert.ToDecimal(0))));

            CreateMap<RestaurantCreateModel, RestaurantEntity>()
                .Ignore(dst => dst.Dishes)
                .Ignore(dst => dst.Orders);

            CreateMap<RestaurantEntity, FilterModel>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.LogoUrl))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => FilterType.Restaurant));
        }
    }
}
