using System;
using System.Collections.Generic;
using System.Linq;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Api.DAL.Common.Repositories;

namespace Delivery.Api.DAL.Memory.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IList<OrderEntity> orders;
        private readonly IList<DishEntity> dishes;
        private readonly IList<DishAmountEntity> dishAmounts;
        private readonly IList<RestaurantEntity> restaurants;

        public OrderRepository(
            Storage storage)
        {
            this.orders = storage.Orders;
            this.dishes = storage.Dishes;
            this.dishAmounts = storage.DishAmounts;
            this.restaurants = storage.Restaurants;

        }

        public IList<OrderEntity> GetAll()
        {
            var orders = this.orders;

            foreach (var order in orders)
            {
                order.DishAmounts = GetDishAmountsByOrderId(order.Id);
                foreach (var dishAmount in order.DishAmounts)
                {
                    dishAmount.Dish = GetDishByDishAmountId(dishAmount.Id);
                }
            }

            return orders;
        }

        public OrderEntity? GetById(Guid id)
        {
            var orderEntity = orders.SingleOrDefault(order => order.Id == id);

            if (orderEntity is not null)
            {
                orderEntity.Restaurant = GetRestaurantByOrderId(id);
                orderEntity.DishAmounts = GetDishAmountsByOrderId(orderEntity.Id);
                foreach (var dishAmount in orderEntity.DishAmounts)
                {
                    dishAmount.Dish = GetDishByDishAmountId(dishAmount.Id);
                }
            }

            return orderEntity;
        }

        public Guid Insert(OrderEntity entity)
        {
            orders.Add(entity);

            foreach (var dishAmount in entity.DishAmounts)
            {
                var dishAmountEntity = new DishAmountEntity(dishAmount.Id, dishAmount.Amount, dishAmount.DishId, entity.Id);
                dishAmounts.Add(dishAmountEntity);
            }

            return entity.Id;
        }

        public Guid? Update(OrderEntity entity)
        {
            var updatedEntity = orders.SingleOrDefault(order => order.Id == entity.Id);

            if (updatedEntity is not null)
            {
                updatedEntity.Address = entity.Address;
                updatedEntity.DeliveryTime = entity.DeliveryTime;
                updatedEntity.Note = entity.Note;
                updatedEntity.State = entity.State;
                updatedEntity.RestaurantId = entity.RestaurantId;
                updatedEntity.Restaurant = getRestaurantById(entity.RestaurantId);
                UpdateDishAmounts(entity, updatedEntity);
            }

            return updatedEntity?.Id;
        }
        private void UpdateDishAmounts(OrderEntity updatedEntity, OrderEntity existingEntity)
        {
            var dishAmountsToDelete = existingEntity.DishAmounts.Where(t =>
                !updatedEntity.DishAmounts.Select(a => a.Id).Contains(t.Id));
            DeleteDishAmounts(dishAmountsToDelete);

            var orderUpdateDishModelsToInsert = updatedEntity.DishAmounts.Where(t =>
                !existingEntity.DishAmounts.Select(a => a.Id).Contains(t.Id));
            InsertDishAmounts(existingEntity, orderUpdateDishModelsToInsert);

            var orderUpdateDishModelsToUpdate = updatedEntity.DishAmounts.Where(
                dish => existingEntity.DishAmounts.Any(ia => ia.DishId == dish.DishId));
            UpdateDishAmounts(existingEntity, orderUpdateDishModelsToUpdate);
        }

        private void UpdateDishAmounts(OrderEntity orderEntity,
            IEnumerable<DishAmountEntity> orderDishModelsToUpdate)
        {
            foreach (var orderUpdateDishModel in orderDishModelsToUpdate)
            {
                DishAmountEntity? dishAmountEntity;
                if (orderUpdateDishModel.Id == Guid.Empty)
                {
                    dishAmountEntity = GetDishAmountOrderIdAndDishId(orderEntity.Id,
                        orderUpdateDishModel.DishId);
                }
                else
                {
                    dishAmountEntity = dishAmounts.Single(t => t.Id == orderUpdateDishModel.Id);
                }

                if (dishAmountEntity is not null)
                {
                    dishAmountEntity.Amount = orderUpdateDishModel.Amount;
                    dishAmountEntity.DishId = orderUpdateDishModel.DishId;
                }
            }
        }

        private void DeleteDishAmounts(IEnumerable<DishAmountEntity> dishAmountsToDelete)
        {
            var toDelete = dishAmountsToDelete.ToList();
            for (int i = 0; i < toDelete.Count; i++)
            {
                var dishAmountEntity = toDelete.ElementAt(i);
                dishAmounts.Remove(dishAmountEntity);
            }
        }

        private void InsertDishAmounts(OrderEntity existingEntity,
            IEnumerable<DishAmountEntity> orderDishModelsToInsert)
        {
            foreach (var dishModel in orderDishModelsToInsert)
            {
                var dishAmountEntity = new DishAmountEntity(dishModel.Id, dishModel.Amount, dishModel.DishId,
                    existingEntity.Id)
                { OrderId = existingEntity.Id };

                dishAmounts.Add(dishAmountEntity);
            }
        }

        private IList<DishAmountEntity> GetDishAmountsByOrderId(Guid orderId)
        {
            return dishAmounts.Where(dishAmountEntity => dishAmountEntity.OrderId == orderId).ToList();
        }

        private DishAmountEntity? GetDishAmountOrderIdAndDishId(Guid orderId, Guid dishId)
        {
            return dishAmounts.SingleOrDefault(entity => entity.OrderId == orderId && entity.DishId == dishId);
        }

        public bool Remove(Guid id)
        {
            var dishAmountsToRemove = dishAmounts.Where(dishAmount => dishAmount.OrderId == id).ToList();

            for (var i = 0; i < dishAmountsToRemove.Count; i++)
            {
                var dishAmountToRemove = dishAmountsToRemove.ElementAt(i);
                dishAmounts.Remove(dishAmountToRemove);
            }

            var orderToRemove = orders.SingleOrDefault(orderEntity => orderEntity.Id == id);
            if (orderToRemove is not null)
            {
                return orders.Remove(orderToRemove);
            }

            return false;
        }

        public bool Exists(Guid id)
        {
            return orders.Any(order => order.Id == id);
        }

        private DishEntity? GetDishByDishAmountId(Guid dishAmountId)
        {
            var dishAmount = dishAmounts.SingleOrDefault(dishAmountEntity => dishAmountEntity.Id == dishAmountId);
            if (dishAmount is not null)
            {
                return dishes.SingleOrDefault(dishEntity => dishEntity.Id == dishAmount.DishId);
            }

            return null;
        }

        private RestaurantEntity? GetRestaurantByOrderId(Guid orderId)
        {
            var order = orders.SingleOrDefault(orderEntity => orderEntity.Id == orderId);
            if (order is not null)
            {
                return restaurants.SingleOrDefault(restaurantEntity => restaurantEntity.Id == order.RestaurantId);
            }

            return null;
        }

        private RestaurantEntity? getRestaurantById(Guid restaurantId)
        {
            return restaurants.SingleOrDefault(restaurantEntity => restaurantEntity.Id == restaurantId);
        }
    }
}
