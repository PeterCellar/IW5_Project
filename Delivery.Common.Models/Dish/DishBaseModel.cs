using System.ComponentModel.DataAnnotations;
using Delivery.Common.Enums;
using Delivery.Common.Models.OrderDish;
using Delivery.Common.Models.Resources;

namespace Delivery.Common.Models.Dish
{
    public record DishBaseModel : IWithId
    {
        public Guid Id { get; init; }

        [Required(ErrorMessageResourceName = nameof(DishDetailModelResources.Name_Required_ErrorMessage), ErrorMessageResourceType = typeof(DishDetailModelResources))]
        public required string Name { get; set; }

        [Required(ErrorMessageResourceName = nameof(DishDetailModelResources.Description_Required_ErrorMessage), ErrorMessageResourceType = typeof(DishDetailModelResources))]
        [MinLength(10, ErrorMessageResourceName = nameof(DishDetailModelResources.Description_MinLength_ErrorMessage), ErrorMessageResourceType = typeof(DishDetailModelResources))]
        public required string Description { get; set; }

        [Required(ErrorMessageResourceName = nameof(DishDetailModelResources.Price_Required_ErrorMessage), ErrorMessageResourceType = typeof(DishDetailModelResources))]
        public required Decimal Price { get; set; }

        public required ICollection<Allergen> Allergens { get; set; }

        public string? ImageUrl { get; set; }
    }
}
