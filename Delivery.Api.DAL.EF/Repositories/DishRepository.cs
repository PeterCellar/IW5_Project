using System;
using System.Collections.Generic;
using System.Linq;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Api.DAL.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Api.DAL.EF.Repositories
{
    public class DishRepository : RepositoryBase<DishEntity>, IDishRepository
    {
        public DishRepository(DeliveryDbContext dbContext)
            : base(dbContext)
        {
        }

        public override DishEntity? GetById(Guid id)
        {
            return dbContext.Dishes
                .Include(dish => dish.Restaurant)
                .Include(dish => dish.Allergens)
                .SingleOrDefault(entity => entity.Id == id);
        }

        public List<DishEntity> GetBySubstring(string substring)
        {
            return dbContext.Dishes
                .Include(dish => dish.Restaurant)
                .Include(dish => dish.Allergens)
                .Where(entity => entity.Name.Contains(substring) || entity.Description.Contains(substring)).ToList();
        }
    }
}
