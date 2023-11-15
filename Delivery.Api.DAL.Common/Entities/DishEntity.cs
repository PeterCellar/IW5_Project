using System.Collections.ObjectModel;
using AutoMapper;
using Delivery.Common.Enums;

namespace Delivery.Api.DAL.Common.Entities
{
    public record DishEntity : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public Guid RestaurantId { get; set; }
        public RestaurantEntity? Restaurant { get; set; }
        public string? ImageUrl { get; set; }

        public IEnumerable<DishAllergenEntity> Allergens { get; set; } = new List<DishAllergenEntity>();
        public ICollection<DishAmountEntity> DishAmounts { get; set; } = new List<DishAmountEntity>();

        public DishEntity(Guid id, string name, string description, Decimal price, Guid restaurantId, string? imageUrl = null) : base(id)
        {
            Name = name;
            Description = description;
            Price = price;
            RestaurantId = restaurantId;
            ImageUrl = imageUrl;
        }
    }

    public class DishEntityMapperProfile : Profile
    {
        public DishEntityMapperProfile()
        {
            CreateMap<DishEntity, DishEntity>();
        }
    }
}
