using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Delivery.Api.BL.Facades.Interfaces;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Api.DAL.Common.Repositories;
using Delivery.Common.Models.Order;
using Delivery.Common.Models.OrderDish;

namespace Delivery.Api.BL.Facades
{
    public class OrderFacade : IOrderFacade
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public OrderFacade(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        public Guid Create(OrderCreateModel orderModel)
        {
            MergeDishAmounts(orderModel);
            var orderEntity = mapper.Map<OrderEntity>(orderModel);
            return orderRepository.Insert(orderEntity);
        }

        public Guid CreateOrUpdate(OrderCreateModel orderModel)
        {
            return orderRepository.Exists(orderModel.Id)
                ? Update(orderModel)!.Value
                : Create(orderModel);
        }

        public bool Delete(Guid id)
        {
            return orderRepository.Remove(id);
        }

        public List<OrderListModel> GetAll()
        {
            var orderEntities = orderRepository.GetAll();
            return mapper.Map<List<OrderListModel>>(orderEntities); 
        }

        public OrderDetailModel? GetById(Guid id)
        {
            var orderEntity = orderRepository.GetById(id);
            return mapper.Map<OrderDetailModel>(orderEntity);
        }

        public Guid? Update(OrderCreateModel orderModel)
        {
            MergeDishAmounts(orderModel);

            var orderEntity = mapper.Map<OrderEntity>(orderModel);

            orderEntity.DishAmounts = orderModel.DishAmounts.Select(t =>
                new DishAmountEntity
                (
                    t.Id,
                    t.Amount,
                    t.DishId,
                    orderEntity.Id
                )).ToList();

            var result = orderRepository.Update(orderEntity);
            return result;
        }

        public void MergeDishAmounts(OrderCreateModel order)
        {
            var result = new List<OrderDishCreateModel>();
            var orderAmountGroups = order.DishAmounts.GroupBy(t => $"{t.DishId}");

            foreach (var orderAmountGroup in orderAmountGroups)
            {
                var orderAmountFirst = orderAmountGroup.First();
                var totalAmount = orderAmountGroup.Sum(t => t.Amount);
                var orderAmount = orderAmountFirst with { Amount = totalAmount };

                result.Add(orderAmount);
            }

            order.DishAmounts = result;
        }
    }
}
