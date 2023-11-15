using Delivery.Common.Models.Restaurant;

namespace Delivery.Common.Models.Dish
{
    public record DishDetailModel : DishBaseModel
    {
        public RestaurantListModel? Restaurant { get; set; }
    }
}
