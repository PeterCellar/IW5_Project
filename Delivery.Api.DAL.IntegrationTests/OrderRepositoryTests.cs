using System;
using System.Collections.Generic;
using System.Linq;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Common.Enums;
using Xunit;

namespace Delivery.API.DAL.IntegrationTests;

public class OrderRepositoryTests
{
    public OrderRepositoryTests()
    {
        _dbFixture = new InMemoryDatabaseFixture();
    }

    private readonly IDatabaseFixture _dbFixture;

    [Fact]
    public void GetById_Returns_Requested_Order_Including_Their_DishAmounts()
    {
        //arrange
        var orderRepository = _dbFixture.GetOrderRepository();

        //act
        var order = orderRepository.GetById(_dbFixture.OrderGuids[0]);

        //assert
        Assert.NotNull(order);
        Assert.Equal(_dbFixture.OrderGuids[0], order.Id);
        Assert.Equal("Fast please", order.Note);

        Assert.Equal(_dbFixture.RestaurantGuids[0], order.RestaurantId);

        Assert.Equal(2, order.DishAmounts.Count);
        var dishAmount1 = Assert.Single(order.DishAmounts.Where(entity => entity.Id == _dbFixture.DishAmountGuids[0]));
        var dishAmount2 = Assert.Single(order.DishAmounts.Where(entity => entity.Id == _dbFixture.DishAmountGuids[1]));

        Assert.Equal(_dbFixture.OrderGuids[0], dishAmount1.OrderId);
        Assert.Equal(_dbFixture.OrderGuids[0], dishAmount2.OrderId);

        Assert.NotNull(dishAmount1.Dish);
        Assert.Equal("Salami", dishAmount1.Dish!.Description);

        Assert.NotNull(dishAmount2.Dish);
        Assert.Equal("Funghi", dishAmount2.Dish!.Description);
    }

    [Fact]
    public void Insert_Saves_Order_And_DishAmounts()
    {
        //arrange
        var orderRepository = _dbFixture.GetOrderRepository();

        var dishAmountId = Guid.NewGuid();
        var orderId = Guid.NewGuid();
        var deliveryTime = TimeSpan.FromMinutes(37);
        var newOrder = new OrderEntity(orderId, "542 Jewell Road", deliveryTime, "TestOrder", _dbFixture.RestaurantGuids[0])
        {
            DishAmounts = new List<DishAmountEntity>()
            {
                new DishAmountEntity(dishAmountId, 3, _dbFixture.DishGuids[1], orderId)
            }
        };

        //act
        orderRepository.Insert(newOrder);

        //assert
        var order = _dbFixture.GetOrderDirectly(orderId);
        Assert.NotNull(order);
        Assert.Equal(OrderState.Created, order.State);
        Assert.Equal(deliveryTime, order.DeliveryTime);

        var dishAmount = _dbFixture.GetDishAmountDirectly(dishAmountId);
        Assert.NotNull(dishAmount);
        Assert.Equal(orderId, dishAmount.OrderId);
        Assert.Equal(_dbFixture.DishGuids[1], dishAmount.DishId);
    }

    [Fact]
    public void Update_Saves_NewDishAmount()
    {
        //arrange
        var orderRepository = _dbFixture.GetOrderRepository();

        var orderId = _dbFixture.OrderGuids[0];
        var order = _dbFixture.GetOrderDirectly(orderId);
        var dishAmountId = Guid.NewGuid();

        var newDishAmount = new DishAmountEntity(dishAmountId, 4, _dbFixture.DishGuids[0], orderId);

        //act
        order.DishAmounts.Add(newDishAmount);
        orderRepository.Update(order);

        //assert
        var orderFromDb = _dbFixture.GetOrderDirectly(orderId);
        Assert.NotNull(orderFromDb);
        Assert.Single(orderFromDb.DishAmounts.Where(t => t.Id == dishAmountId));

        var dishAmountFromDb = _dbFixture.GetDishAmountDirectly(dishAmountId);
        Assert.NotNull(dishAmountFromDb);
        Assert.Equal(newDishAmount.Amount, dishAmountFromDb.Amount);
        Assert.Equal(_dbFixture.DishGuids[0], dishAmountFromDb.DishId);
    }

    [Fact]
    public void Update_Also_Updates_DishAmount()
    {
        //arrange
        var orderRepository = _dbFixture.GetOrderRepository();

        var orderId = _dbFixture.OrderGuids[0];
        var order = _dbFixture.GetOrderDirectly(orderId);
        var dishAmount = order.DishAmounts.First();

        //act
        dishAmount.Amount = 8;
        orderRepository.Update(order);

        //assert
        var orderFromDb = _dbFixture.GetOrderDirectly(orderId);
        Assert.NotNull(orderFromDb);
        var dishAmountFromDb = orderFromDb.DishAmounts.First();
        Assert.Equal(dishAmount.Id, dishAmountFromDb.Id);
        Assert.Equal(dishAmount.Amount, dishAmountFromDb.Amount);
    }

    [Fact]
    public void Exist_Order()
    {
        //arrange
        var orderRepository = _dbFixture.GetOrderRepository();
        var orderId = _dbFixture.OrderGuids[0];
        var unknownId = Guid.NewGuid();
        
        //assert
        Assert.True(orderRepository.Exists(orderId));
        Assert.False(orderRepository.Exists(unknownId));
    }

    [Fact]
    public void Insert_Then_Remove_Order()
    {
        //arrange
        var orderRepository = _dbFixture.GetOrderRepository();

        var dishAmountId = Guid.NewGuid();
        var orderId = Guid.NewGuid();
        var deliveryTime = TimeSpan.FromMinutes(43);
        var newOrder = new OrderEntity(orderId, "542 East Road", deliveryTime, "TestOrder", _dbFixture.RestaurantGuids[0])
        {
            DishAmounts = new List<DishAmountEntity>()
            {
                new DishAmountEntity(dishAmountId, 3, _dbFixture.DishGuids[1], orderId)
            }
        };

        //act
        orderRepository.Insert(newOrder);

        //assert
        var orderFromDb = _dbFixture.GetOrderDirectly(orderId);
        Assert.NotNull(orderFromDb);
        Assert.Equal(orderId, orderFromDb.Id);
        
        //act
        orderRepository.Remove(orderId);
        
        //assert
        var removedOrderFromDb = _dbFixture.GetOrderDirectly(orderId);
        Assert.Null(removedOrderFromDb);
    }

    [Fact]
    public void GetAll_Orders()
    {
        //arrange
        var orderRepository = _dbFixture.GetOrderRepository();

        //act
        var ordersCount = orderRepository.GetAll().Count();

        //assert
        Assert.Equal(1, ordersCount);
    }
}
