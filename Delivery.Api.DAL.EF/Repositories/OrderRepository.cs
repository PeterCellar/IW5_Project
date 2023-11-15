using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Api.DAL.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Api.DAL.EF.Repositories
{
    public class OrderRepository : RepositoryBase<OrderEntity>, IOrderRepository
    {
        private readonly IMapper mapper;

        public OrderRepository(
            DeliveryDbContext dbContext,
            IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
        }

        public override IList<OrderEntity> GetAll()
        {
            return dbContext.Orders
            .Include(order => order.DishAmounts)
            .ThenInclude(order => order.Dish).ToList();
        }

        public override OrderEntity? GetById(Guid id)
        {
            return dbContext.Orders
                .Include(order => order.Restaurant)
                .Include(order => order.DishAmounts)
                .ThenInclude(dishAmount => dishAmount.Dish)
                .SingleOrDefault(entity => entity.Id == id);
        }

        public override Guid? Update(OrderEntity order)
        {
            if (Exists(order.Id))
            {
                var existingOrder = dbContext.Orders
                    .Include(o => o.DishAmounts)
                    .Single(o => o.Id == order.Id);

                mapper.Map(order, existingOrder);

                dbContext.Orders.Update(existingOrder);
                dbContext.SaveChanges();

                return existingOrder.Id;
            }
            else
            {
                return null;
            }
        }
    }
}
