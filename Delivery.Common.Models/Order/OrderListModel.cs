using Delivery.Common.Enums;

namespace Delivery.Common.Models.Order
{
    public record OrderListModel : IWithId
    {
        public required Guid Id { get; init; }
        public required TimeSpan DeliveryTime { get; set; }
        public required string Note { get; set; }
        public required OrderState State { get; set; }
        public required Guid RestaurantId { get; set; }
        public Decimal? TotalPrice { get; set; }
    }
}
