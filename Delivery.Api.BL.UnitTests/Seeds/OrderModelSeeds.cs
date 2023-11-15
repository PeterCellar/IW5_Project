using Delivery.Common.Enums;
using Delivery.Common.Models;
using Delivery.Common.Models.Order;
using Delivery.Common.Models.OrderDish;

namespace Delivery.Api.BL.UnitTests.Seeds;

public class OrderModelSeeds
{
    public static readonly IList<Guid> OrderGuids = new List<Guid>
    {
        new ("00e43c41-0528-4271-833b-e16b759e16a8"),
        new ("15fc9c0c-8b50-42c6-b2fa-ccd29f60b1ad"),
        new ("292954b5-a499-4cc4-847c-852b6c14eea8")
    };

    public static readonly IList<OrderCreateModel> OrderSeeds = new List<OrderCreateModel>
        {
            new (){
                Id = OrderGuids[0],
                Address = "TestAddress",
                DeliveryTime = TimeSpan.FromMinutes(61),
                Note = "Bla bla",
                State = OrderState.Created,
                DishAmounts = new List<OrderDishCreateModel>()
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Amount = 5,
                        DishId = DishModelSeeds.DishGuids[0],
                        OrderId = OrderGuids[0],
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Amount = 3,
                        DishId = DishModelSeeds.DishGuids[2],
                        OrderId = OrderGuids[0],
                    }
                }
            },
            new (){
                Id = OrderGuids[1],
                Address = "TestAddress2",
                DeliveryTime = TimeSpan.FromMinutes(6),
                Note = "Bla bla2",
                State = OrderState.Created,
                DishAmounts = new List<OrderDishCreateModel>()
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Amount = 7,
                        DishId = DishModelSeeds.DishGuids[1],
                        OrderId = OrderGuids[1],
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Amount = 1,
                        DishId = DishModelSeeds.DishGuids[0],
                        OrderId = OrderGuids[1],
                    }
                }
            }
        };
}
