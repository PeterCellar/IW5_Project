using Delivery.Common.Enums;
using Delivery.Common.Models.Dish;
using Delivery.Common.Models.OrderDish;

namespace Delivery.Api.BL.UnitTests.Seeds;

public class DishModelSeeds
{
    public static readonly IList<Guid> DishGuids = new List<Guid>
    {
        new ("a0e43c41-0528-4271-833b-e16b759e16a8"),
        new ("75fc9c0c-8b50-42c6-b2fa-ccd29f60b1ad"),
        new ("b92954b5-a499-4cc4-847c-852b6c14eea8")
    };
    
    public static readonly IList<DishCreateModel> DishSeeds = new List<DishCreateModel>
        {
            new (){
                Id = DishGuids[0],
                Name = "Test", 
                Description = "Test",
                Price = 1,
                Allergens = new List<Allergen>() { Allergen.Eggs , Allergen.Milk}
            },
            new (){
                Id = DishGuids[1],
                Name = "Test2",
                Description = "Test2",
                Price = 2,
                Allergens = new List<Allergen>() { }
            },
            new(){
                Id = DishGuids[2],
                Name = "Test3", 
                Description = "Test3",
                Price = 3,
                Allergens = new List<Allergen>() { Allergen.Fish, Allergen.Mustard, Allergen.Peanuts }
            }
        };
}
