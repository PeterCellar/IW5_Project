using System;
using AutoMapper;
using Delivery.Common.Enums;

namespace Delivery.Api.DAL.Common.Entities
{
    public record DishAmountEntity : EntityBase
    {
        public int Amount { get; set; }

        public Guid DishId { get; set; }
        public DishEntity? Dish { get; set; }

        public Guid OrderId { get; set; }
        public OrderEntity? Order { get; set; }

        public DishAmountEntity() { }
        public DishAmountEntity(Guid id, int amount, Guid dishId, Guid orderId) : base(id)
        {
            Amount = amount;
            DishId = dishId;
            OrderId = orderId;
        }
    }
}

