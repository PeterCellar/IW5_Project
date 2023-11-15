using Delivery.Common.Models.Dish;
using Delivery.Common.Models.Order;

namespace Delivery.Common.Models.OrderDish
{
    public record OrderDishDetailModel : OrderDishBaseModel
    {
        public required DishListModel Dish { get; set; }
        public required OrderListModel Order { get; set; }
    }
}
