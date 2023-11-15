namespace Delivery.Common.Models.Dish
{
    public record DishCreateModel : DishBaseModel
    {
        public Guid? RestaurantId { get; set; }

        public static implicit operator DishCreateModel(DishDetailModel v)
        {
            throw new NotImplementedException();
        }
    }
}
