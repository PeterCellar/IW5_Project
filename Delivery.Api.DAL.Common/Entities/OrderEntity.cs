using Delivery.Common.Enums;

namespace Delivery.Api.DAL.Common.Entities
{
    public record OrderEntity : EntityBase
    {
        public string Address { get; set; }
        public TimeSpan DeliveryTime { get; set; }
        public string Note { get; set; }
        public OrderState State { get; set; }
        public Guid RestaurantId { get; set; }
        public RestaurantEntity? Restaurant { get; set; }

        public ICollection<DishAmountEntity> DishAmounts { get; set; } = new List<DishAmountEntity>();

        public OrderEntity(Guid id, string address, TimeSpan deliveryTime, string note, Guid restaurantId) : base(id)
        {
            Address = address;
            DeliveryTime = deliveryTime;
            Note = note;
            State = OrderState.Created;
            RestaurantId = restaurantId;
        }
    }
}
