namespace Delivery.Common.Models.OrderDish
{
    public record OrderDishCreateModel : OrderDishBaseModel
    {
        public required Guid DishId { get; set; }
        public required Guid OrderId { get; set; }
    }
}
