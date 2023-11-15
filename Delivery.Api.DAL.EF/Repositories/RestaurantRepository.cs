using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Api.DAL.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Api.DAL.EF.Repositories
{
    public class RestaurantRepository : RepositoryBase<RestaurantEntity>, IRestaurantRepository
    {
        private readonly IMapper mapper;

        public RestaurantRepository(
            DeliveryDbContext dbContext,
            IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
        }

        public override RestaurantEntity? GetById(Guid id)
        {
            return dbContext.Restaurants
                .Include(restaurant => restaurant.Dishes)
                .Include(restaurant => restaurant.Orders)
                .ThenInclude(order => order.DishAmounts)
                .ThenInclude(dishAmount => dishAmount.Dish)
                .SingleOrDefault(entity => entity.Id == id);
        }

        public override Guid? Update(RestaurantEntity restaurant)
        {
            if (Exists(restaurant.Id))
            {
                var existingRestaurant = dbContext.Restaurants
                    .Include(r => r.Dishes)
                    .Single(r => r.Id == restaurant.Id);

                mapper.Map(restaurant, existingRestaurant);

                dbContext.Restaurants.Update(existingRestaurant);
                dbContext.SaveChanges();

                return existingRestaurant.Id;
            }
            else
            {
                return null;
            }
        }

        public List<RestaurantEntity> GetBySubstring(string substring)
        {
            return dbContext.Restaurants
                .Include(restaurant => restaurant.Dishes)
                .Include(restaurant => restaurant.Orders)
                .ThenInclude(order => order.DishAmounts)
                .ThenInclude(dishAmount => dishAmount.Dish)
                .Where(entity => entity.Name.Contains(substring) || entity.Description.Contains(substring) || entity.Address.Contains(substring)).ToList();
        }
    }
}
