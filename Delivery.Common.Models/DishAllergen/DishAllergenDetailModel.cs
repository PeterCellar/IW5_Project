using Delivery.Common.Enums;
using Delivery.Common.Models.Dish;

namespace Delivery.Common.Models.DishAllergen
{
    public record DishAllergenDetailModel : DishAllergenBaseModel
    {
        public required DishListModel Dish { get; set; }
        public required Allergen Allergen { get; set; }
    }
}
