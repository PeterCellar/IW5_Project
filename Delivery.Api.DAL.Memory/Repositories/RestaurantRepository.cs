using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Api.DAL.Common.Repositories;

namespace Delivery.Api.DAL.Memory.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly IList<DishEntity> dishes;
        private readonly IList<DishAmountEntity> dishAmounts;
        private readonly IList<OrderEntity> orders;
        private readonly IList<RestaurantEntity> restaurants;
        private readonly IMapper mapper;

        public RestaurantRepository(
            Storage storage,
            IMapper mapper)
        {
            this.dishes = storage.Dishes;
            this.orders = storage.Orders;
            this.restaurants = storage.Restaurants;
            this.dishAmounts = storage.DishAmounts;
            this.mapper = mapper;
        }

        public IList<RestaurantEntity> GetAll()
        {
            return restaurants;
        }

        public RestaurantEntity? GetById(Guid id)
        {
            var restaurantEntity = restaurants.SingleOrDefault(entity => entity.Id == id);

            if (restaurantEntity is not null)
            {
                restaurantEntity.Dishes = GetDishesByRestaurantId(id);
                restaurantEntity.Orders = GetOrdersByRestaurantId(id);

                foreach (var dish in restaurantEntity.Dishes)
                {
                    dish.DishAmounts = GetDishAmountsByDishId(dish.Id);
                    foreach (var dishAmount in dish.DishAmounts)
                    {
                        dishAmount.Order = GetOrderByDishAmountId(dishAmount.Id);
                    }
                }

                foreach (var order in restaurantEntity.Orders)
                {
                    order.DishAmounts = GetDishAmountsByOrderId(order.Id);
                    foreach (var dishAmount in order.DishAmounts)
                    {
                        dishAmount.Dish = GetDishByDishAmountId(dishAmount.Id);
                    }
                }
            }

            return restaurantEntity;
        }

        public Guid Insert(RestaurantEntity restaurant)
        {
            restaurants.Add(restaurant);

            foreach (var dish in restaurant.Dishes)
            {
                dish.RestaurantId = restaurant.Id;
                dishes.Add(dish);
            }

            foreach (var order in restaurant.Orders)
            {
                order.RestaurantId = restaurant.Id;
                orders.Add(order);
            }

            return restaurant.Id;
        }

        public Guid? Update(RestaurantEntity entity)
        {
            var restaurantExisting = restaurants.SingleOrDefault(restaurantInStorage =>
                restaurantInStorage.Id == entity.Id);
            if (restaurantExisting != null)
            {
                mapper.Map(entity, restaurantExisting);
            }

            return restaurantExisting?.Id;
        }

        public bool Remove(Guid id)
        {
            var restaurantToRemove = restaurants.Single(restaurant => restaurant.Id.Equals(id));
            return restaurants.Remove(restaurantToRemove);
        }

        public bool Exists(Guid id)
        {
            return restaurants.Any(restaurant => restaurant.Id == id);
        }

        public List<RestaurantEntity> GetBySubstring(string substring)
        {
            return restaurants
                .Where(entity => entity.Name.Contains(substring) || entity.Description.Contains(substring) || entity.Address.Contains(substring)).ToList();
        }

        private List<DishEntity> GetDishesByRestaurantId(Guid restaurantId)
        {
            return dishes.Where(dish => dish.RestaurantId == restaurantId).ToList();
        }

        private List<OrderEntity> GetOrdersByRestaurantId(Guid restaurantId)
        {
            return orders.Where(order => order.RestaurantId == restaurantId).ToList();
        }

        private List<DishAmountEntity> GetDishAmountsByDishId(Guid dishId)
        {
            return dishAmounts.Where(dishAmount => dishAmount.DishId == dishId).ToList();
        }

        private List<DishAmountEntity> GetDishAmountsByOrderId(Guid orderId)
        {
            return dishAmounts.Where(dishAmount => dishAmount.OrderId == orderId).ToList();
        }

        private OrderEntity? GetOrderByDishAmountId(Guid dishAmountId)
        {
            var dishAmount = dishAmounts.SingleOrDefault(dishAmount => dishAmount.Id == dishAmountId);
            return dishAmount != null ? orders.SingleOrDefault(order => order.Id == dishAmount.OrderId) : null;
        }

        private DishEntity? GetDishByDishAmountId(Guid dishAmountId)
        {
            var dishAmount = dishAmounts.SingleOrDefault(dishAmount => dishAmount.Id == dishAmountId);
            return dishAmount != null ? dishes.SingleOrDefault(dish => dish.Id == dishAmount.DishId) : null;
        }
    }
}
