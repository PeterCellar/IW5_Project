using Delivery.Api.DAL.Common.Entities;

namespace Delivery.Api.DAL.Common.Repositories
{
    public interface IDishRepository : IApiRepository<DishEntity>
    {
        List<DishEntity> GetBySubstring(string substring);
    }
}
