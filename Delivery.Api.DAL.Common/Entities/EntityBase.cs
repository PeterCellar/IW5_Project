using System;
using Delivery.Api.DAL.Common.Entities.Interfaces;

namespace Delivery.Api.DAL.Common.Entities
{
    public abstract record EntityBase : IEntity
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        protected EntityBase()
        {
        }

        protected EntityBase(Guid id)
        {
            Id = id;
        }
    }
}
