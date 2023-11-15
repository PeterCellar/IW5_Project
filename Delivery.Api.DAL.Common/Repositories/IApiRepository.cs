using System;
using System.Collections.Generic;
using Delivery.Api.DAL.Common.Entities.Interfaces;

namespace Delivery.Api.DAL.Common.Repositories
{
    public interface IApiRepository<TEntity>
        where TEntity : IEntity
    {
        IList<TEntity> GetAll();
        TEntity? GetById(Guid id);
        Guid Insert(TEntity entity);
        Guid? Update(TEntity entity);
        bool Remove(Guid id);
        bool Exists(Guid id);
    }
}
