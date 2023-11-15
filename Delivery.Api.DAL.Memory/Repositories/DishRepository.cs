using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Api.DAL.Common.Repositories;

namespace Delivery.Api.DAL.Memory.Repositories
{
    public class DishRepository : IDishRepository
    {
        private readonly IList<DishEntity> dishes;
        private readonly IList<RestaurantEntity> restaurants;
        private readonly IList<DishAmountEntity> dishAmounts;
        private readonly IList<DishAllergenEntity> dishAllergens;
        private readonly IMapper mapper;

        public DishRepository(
            Storage storage,
            IMapper mapper)
        {
            this.dishes = storage.Dishes;
            this.restaurants = storage.Restaurants;
            this.dishAmounts = storage.DishAmounts;
            this.dishAllergens = storage.DishAlergens;
            this.mapper = mapper;
        }

        public IList<DishEntity> GetAll()
        {
            return dishes;
        }

        public DishEntity? GetById(Guid id)
        {
            var dishEntity = dishes.SingleOrDefault(entity => entity.Id == id);

            if (dishEntity is not null)
            {
                dishEntity.Restaurant = GetRestaurantById(dishEntity.RestaurantId);
                dishEntity.Allergens = GetAllergensByDishId(id);
            }

            return dishEntity;
        }

        public Guid Insert(DishEntity dish)
        {
            dishes.Add(dish);
            foreach (var dishAmount in dish.DishAmounts)
            {
                dishAmounts.Add(dishAmount);
            }

            foreach (var dishAllergen in dish.Allergens)
            {
                dishAllergens.Add(dishAllergen);
            }
            return dish.Id;
        }

        public Guid? Update(DishEntity entity)
        {
            var dishExisting = dishes.SingleOrDefault(dishInStorage =>
                dishInStorage.Id == entity.Id);
            if (dishExisting != null)
            {
                mapper.Map(entity, dishExisting);
            }

            return dishExisting?.Id;
        }

        public bool Remove(Guid id)
        {
            var dishAmountsToRemove =
                dishAmounts.Where(dishAmount => dishAmount.DishId == id).ToList();

            for (var i = 0; i < dishAmountsToRemove.Count; i++)
            {
                var dishAmountToRemove = dishAmountsToRemove.ElementAt(i);
                dishAmounts.Remove(dishAmountToRemove);
            }

            var dishToRemove = dishes.Single(dish => dish.Id.Equals(id));
            return dishes.Remove(dishToRemove);
        }

        public bool Exists(Guid id)
        {
            return dishes.Any(dish => dish.Id == id);
        }

        public List<DishEntity> GetBySubstring(string substring)
        {
            return dishes
                .Where(entity => entity.Name.Contains(substring) || entity.Description.Contains(substring)).ToList();

        }

        private RestaurantEntity? GetRestaurantById(Guid id)
        {
            return restaurants.SingleOrDefault(restaurant => restaurant.Id == id);
        }

        private List<DishAllergenEntity> GetAllergensByDishId(Guid dishId)
        {
            return dishAllergens.Where(dishAllergen => dishAllergen.DishId == dishId).ToList();
        }

    }
}
