using Delivery.Common.Models.Restaurant;

namespace Delivery.Api.BL.UnitTests.Seeds;

public class RestaurantModelSeeds
{
    public static readonly IList<Guid> RestaurantGuids = new List<Guid>
    {
        new ("e0e43c41-0528-4271-833b-e16b759e16a8"),
        new ("15fc9c0c-8b50-42c6-b2fa-ccd29f60b1ad"),
    };
    
    public IList<RestaurantCreateModel> DishSeeds = new List<RestaurantCreateModel>
        {
            new (){
                Id = RestaurantGuids[0],
                Name = "Test", 
                Description = "Test",
                Address = "TestAddress",
                Latitude = 4.15151,
                Longitude = 5.141414
            },
            new (){
                Id = RestaurantGuids[1],
                Name = "Test2", 
                Description = "Test2",
                Address = "TestAddress",
                Latitude = 4.15151,
                Longitude = 5.141414,
                LogoUrl = "Test2Logo"
            },
        };
}
