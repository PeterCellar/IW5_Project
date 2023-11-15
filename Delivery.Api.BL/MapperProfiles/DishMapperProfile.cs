using AutoMapper;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Common.Enums;
using Delivery.Common.Extensions;
using Delivery.Common.Models;
using Delivery.Common.Models.Dish;
using Delivery.Common.Models.OrderDish;

namespace Delivery.Api.BL.MapperProfiles
{
    public class DishMapperProfile : Profile
    {
        public DishMapperProfile()
        {
            CreateMap<DishEntity, DishListModel>();
            CreateMap<DishEntity, DishDetailModel>()
                .ForMember(dest => dest.Allergens, opt => opt.MapFrom(src => src.Allergens!.Select(x => x.Allergen)));

            CreateMap<DishCreateModel, DishEntity>()
                .ForMember(dest => dest.Allergens, opt => opt.MapFrom(src => src.Allergens.Select(x => new DishAllergenEntity(Guid.NewGuid(), x, src.Id))))
                .Ignore(dst => dst.Restaurant)
                .Ignore(dst => dst.DishAmounts);

            CreateMap<DishAmountEntity, OrderDishDetailModel>();

            CreateMap<OrderDishCreateModel, DishAmountEntity>()
                .Ignore(dst => dst.Dish)
                .Ignore(dst => dst.Order);

            CreateMap<DishEntity, FilterModel>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => FilterType.Dish));
        }
    }
}
