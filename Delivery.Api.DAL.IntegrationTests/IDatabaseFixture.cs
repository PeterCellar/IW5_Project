using System;
using System.Collections.Generic;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Api.DAL.Common.Repositories;

namespace Delivery.API.DAL.IntegrationTests;

public interface IDatabaseFixture
{
    DishAmountEntity? GetDishAmountDirectly(Guid dishAmountId);
    OrderEntity? GetOrderDirectly(Guid orderId);
    DishEntity? GetDishDirectly(Guid dishId);
    RestaurantEntity? GetRestaurantDirectly(Guid restaurantId);
    IOrderRepository GetOrderRepository();
    IDishRepository GetDishRepository();
    IRestaurantRepository GetRestaurantRepository();
    IList<Guid> OrderGuids { get; }
    IList<Guid> DishGuids { get; }
    IList<Guid> DishAmountGuids { get; }
    IList<Guid> RestaurantGuids { get; }
}
