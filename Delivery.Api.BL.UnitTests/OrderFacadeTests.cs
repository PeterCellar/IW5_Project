using AutoMapper;
using Delivery.Api.BL.Facades;
using Delivery.Api.BL.UnitTests.Seeds;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Api.DAL.Common.Repositories;
using Delivery.Common.Enums;
using Delivery.Common.Models.Dish;
using Delivery.Common.Models.Order;
using Delivery.Common.Models.OrderDish;
using Moq;

namespace Delivery.Api.BL.UnitTests
{
    public class OrderFacadeTests
    {
        private static OrderFacade GetFacadeWithForbiddenDependencyCalls()
        {
            var repository = new Mock<IOrderRepository>(MockBehavior.Strict).Object;
            var mapper = new Mock<IMapper>(MockBehavior.Strict).Object;
            var facade = new OrderFacade(repository, mapper);
            return facade;
        }

        [Fact]
        public void Delete_Calls_Correct_Method_On_Repository()
        {
            var repositoryMock = new Mock<IOrderRepository>(MockBehavior.Loose);
            repositoryMock.Setup(orderRepository => orderRepository.Remove(It.IsAny<Guid>()));

            var repository = repositoryMock.Object;
            var mapper = new Mock<IMapper>(MockBehavior.Loose).Object;
            var facade = new OrderFacade(repository, mapper);

            var itemId = Guid.NewGuid();

            facade.Delete(itemId);

            repositoryMock.Verify(orderRepository => orderRepository.Remove(itemId));
        }

        [Fact]
        public void GetAll_Calls_Correct_Method_On_Repository()
        {
            var repositoryMock = new Mock<IOrderRepository>(MockBehavior.Loose);
            repositoryMock.Setup(orderRepository => orderRepository.GetAll());

            var repository = repositoryMock.Object;
            var mapper = new Mock<IMapper>(MockBehavior.Loose).Object;
            var facade = new OrderFacade(repository, mapper);

            facade.GetAll();

            repositoryMock.Verify(orderRepository => orderRepository.GetAll());
        }

        [Fact]
        public void GetById_Calls_Correct_Method_On_Repository()
        {
            var repositoryMock = new Mock<IOrderRepository>(MockBehavior.Loose);
            repositoryMock.Setup(orderRepository => orderRepository.GetById(It.IsAny<Guid>()));

            var repository = repositoryMock.Object;
            var mapper = new Mock<IMapper>(MockBehavior.Loose).Object;
            var facade = new OrderFacade(repository, mapper);

            var itemId = Guid.NewGuid();
            facade.GetById(itemId);

            repositoryMock.Verify(orderRepository => orderRepository.GetById(itemId));
        }

        [Fact]
        public void Create_Calls_Correct_Method_On_Repository()
        {
            var repositoryMock = new Mock<IOrderRepository>(MockBehavior.Loose);
            repositoryMock.Setup(orderRepository => orderRepository.Insert(It.IsAny<OrderEntity>()));

            var repository = repositoryMock.Object;
            var mapper = new Mock<IMapper>(MockBehavior.Loose).Object;
            var facade = new OrderFacade(repository, mapper);

            var orderModel = new OrderCreateModel()
            {
                Id = Guid.NewGuid(),
                Address = "Test",
                DeliveryTime = TimeSpan.FromSeconds(1),
                Note = "Test",
                State = OrderState.Created,
                RestaurantId = Guid.Parse("cff8b2a5-2ddb-4584-b3fe-101a13956d4c"),
            };

            facade.Create(orderModel);

            var orderEntity = mapper.Map<OrderEntity>(orderModel);
            repositoryMock.Verify(orderRepository => orderRepository.Insert(orderEntity));
        }

        [Fact]
        public void MergeDishAmounts_Does_Merge_Order_With_Multiple_DishAmounts_Of_Same_Dish()
        {
            //arrange
            var facade = GetFacadeWithForbiddenDependencyCalls();

            var mergedDishId = DishModelSeeds.DishGuids[0];
            var dishAmount1Id = Guid.NewGuid();
            var dishAmount2Id = Guid.NewGuid();

            var orderId = Guid.NewGuid();
            var order = new OrderCreateModel()
            {
                Id = orderId,
                Address = "TestAddress",
                DeliveryTime = TimeSpan.FromMinutes(61),
                Note = "Bla bla",
                State = OrderState.Created,
                DishAmounts = new List<OrderDishCreateModel>()
                {
                    new()
                    {
                        Id = dishAmount1Id,
                        Amount = 4,
                        DishId = mergedDishId,
                        OrderId = orderId
                    },
                    new()
                    {
                        Id = dishAmount2Id,
                        Amount = 1,
                        DishId = mergedDishId,
                        OrderId = orderId
                    }
                }
            };

            //act
            facade.MergeDishAmounts(order);

            //assert
            var mergedDish = Assert.Single(order.DishAmounts);

            Assert.Equal(1, order.DishAmounts.Count);
            Assert.Equal(5, mergedDish.Amount);
            Assert.Equal(mergedDishId, mergedDish.DishId);
        }

        [Fact]
        public void MergeDishAmounts_Does_Not_Merge_Order_With_Multiple_DishAmounts_Of_Different_Dish()
        {
            //arrange
            var facade = GetFacadeWithForbiddenDependencyCalls();

            var dishAmount1Id = Guid.NewGuid();
            var dishAmount2Id = Guid.NewGuid();

            var orderId = Guid.NewGuid();
            var order = new OrderCreateModel()
            {
                Id = orderId,
                Address = "TestAddress",
                DeliveryTime = TimeSpan.FromMinutes(61),
                Note = "Bla bla",
                State = OrderState.Created,
                DishAmounts = new List<OrderDishCreateModel>()
                {
                    new()
                    {
                        Id = dishAmount1Id,
                        Amount = 4,
                        DishId = DishModelSeeds.DishGuids[0],
                        OrderId = orderId
                    },
                    new()
                    {
                        Id = dishAmount2Id,
                        Amount = 1,
                        DishId = DishModelSeeds.DishGuids[1],
                        OrderId = orderId
                    }
                }
            };

            //act
            facade.MergeDishAmounts(order);

            //assert
            Assert.Equal(2, order.DishAmounts.Count);
            var dishAmount1 = Assert.Single(order.DishAmounts.Where(t => t.Id == dishAmount1Id));
            var dishAmount2 = Assert.Single(order.DishAmounts.Where(t => t.Id == dishAmount2Id));

            Assert.Equal(4, dishAmount1.Amount);
            Assert.Equal(1, dishAmount2.Amount);
        }

        [Fact]
        public void MergeDishAmounts_Does_Not_Fail_When_Order_Has_No_Dishes()
        {
            var facade = GetFacadeWithForbiddenDependencyCalls();
            var order = new OrderCreateModel()
            {
                Id = Guid.NewGuid(),
                Address = "TestAddress",
                DeliveryTime = TimeSpan.FromMinutes(61),
                Note = "Bla bla",
                State = OrderState.Created,
                DishAmounts = new List<OrderDishCreateModel>() { }
            };

            facade.MergeDishAmounts(order);

            Assert.Empty(order.DishAmounts);
        }
    }
}
