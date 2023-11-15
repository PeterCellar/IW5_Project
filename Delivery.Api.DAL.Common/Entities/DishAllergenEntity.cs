using Delivery.Common.Enums;

namespace Delivery.Api.DAL.Common.Entities
{
    public record DishAllergenEntity
    {
        public Guid Id { get; set; }

        public Allergen Allergen { get; set; }

        public Guid DishId { get; set; }

        public DishEntity? Dish { get; set; }

        public DishAllergenEntity() { }
        public DishAllergenEntity(Guid id, Allergen allergen, Guid dishId)
        {
            Id = id;
            Allergen = allergen;
            DishId = dishId;
        }
    }
}
