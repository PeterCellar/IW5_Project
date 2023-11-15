using AutoMapper;
using Delivery.Common.Enums;
using Delivery.Common.Models.Dish;
using Delivery.Common.Models.Order;
using Delivery.Common.Models.OrderDish;
using Delivery.Common.Models.Restaurant;


namespace Delivery.Web.BL.MapperProfiles
{
    class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<DishDetailModel, DishCreateModel>();
            CreateMap<DishCreateModel, DishDetailModel>();

            CreateMap<OrderDishDetailModel, OrderDishCreateModel>();

            CreateMap<RestaurantDetailModel, RestaurantCreateModel>();
            CreateMap<RestaurantCreateModel, RestaurantDetailModel>();

            CreateMap<OrderDetailModel, OrderCreateModel>();
        }
    }
}
