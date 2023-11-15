using Delivery.Common.Models.OrderDish;

namespace Delivery.Common.Models.Order
{
    public record OrderCreateModel : OrderBaseModel
    {
        public IList<OrderDishCreateModel> DishAmounts { get; set; } = new List<OrderDishCreateModel>();
        public Guid? RestaurantId { get; set; }
    }
}
