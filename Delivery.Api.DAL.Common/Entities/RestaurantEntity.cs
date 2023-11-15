using AutoMapper;

namespace Delivery.Api.DAL.Common.Entities
{
    public record RestaurantEntity : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? LogoUrl { get; set; }
        public ICollection<DishEntity> Dishes { get; set; } = new List<DishEntity>();
        public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();

        public RestaurantEntity(Guid id, string name, string description, string address, double latitude, double longitude, string? logoUrl = null) : base(id)
        {
            Name = name;
            Description = description;
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
            LogoUrl = logoUrl;
        }
    }

    public class RestaurantEntityMapperProfile : Profile
    {
        public RestaurantEntityMapperProfile()
        {
            CreateMap<RestaurantEntity, RestaurantEntity>();
        }
    }
}
