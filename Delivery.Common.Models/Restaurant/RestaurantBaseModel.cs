using System.ComponentModel.DataAnnotations;
using Delivery.Common.Models.Dish;
using Delivery.Common.Models.Order;
using Delivery.Common.Models.Resources;

namespace Delivery.Common.Models.Restaurant
{
    public record RestaurantBaseModel : IWithId
    {
        public Guid Id { get; init; }

        [Required(ErrorMessageResourceName = nameof(RestaurantDetailModelResources.Name_Required_ErrorMessage), ErrorMessageResourceType = typeof(RestaurantDetailModelResources))]
        public required string Name { get; set; }

        [Required(ErrorMessageResourceName = nameof(RestaurantDetailModelResources.Description_Required_ErrorMessage), ErrorMessageResourceType = typeof(RestaurantDetailModelResources))]
        public required string Description { get; set; }

        [Required(ErrorMessageResourceName = nameof(RestaurantDetailModelResources.Address_Required_ErrorMessage), ErrorMessageResourceType = typeof(RestaurantDetailModelResources))]
        public required string Address { get; set; }

        [Required(ErrorMessageResourceName = nameof(RestaurantDetailModelResources.Gps_Required_ErrorMessage), ErrorMessageResourceType = typeof(RestaurantDetailModelResources))]
        public required double Latitude { get; set; }
        public required double Longitude { get; set; }
        public string? LogoUrl { get; set; }
    }
}
