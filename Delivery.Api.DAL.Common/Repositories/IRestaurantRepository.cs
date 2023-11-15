using Delivery.Api.DAL.Common.Entities;

namespace Delivery.Api.DAL.Common.Repositories
{
    public interface IRestaurantRepository : IApiRepository<RestaurantEntity>
    {
        List<RestaurantEntity> GetBySubstring(string substring);
    }
}
