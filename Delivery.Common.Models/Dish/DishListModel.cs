namespace Delivery.Common.Models.Dish
{
    public record DishListModel : IWithId
    {
        public required Guid Id { get; init; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required Decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
