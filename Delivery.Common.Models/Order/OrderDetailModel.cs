using Delivery.Common.Models.OrderDish;
using Delivery.Common.Models.Restaurant;

namespace Delivery.Common.Models.Order
{
    public record OrderDetailModel : OrderBaseModel
    {
        public IList<OrderDishDetailModel> DishAmounts { get; set; } = new List<OrderDishDetailModel>();
        public RestaurantListModel? Restaurant { get; set; }
        public Decimal? TotalPrice { get; set; }
    }
}
