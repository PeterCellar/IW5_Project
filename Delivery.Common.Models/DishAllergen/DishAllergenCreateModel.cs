using Delivery.Common.Enums;

namespace Delivery.Common.Models.DishAllergen
{
    public record DishAllergenCreateModel : DishAllergenBaseModel
    {
        public required Guid DishId { get; set; }
        public required Allergen Allergen { get; set; }
    }
}
