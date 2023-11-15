using Delivery.Common.BL.Facades;
using Delivery.Common.Models.Restaurant;

namespace Delivery.Api.BL.Facades.Interfaces
{
    public interface IRestaurantFacade : IAppFacade
    {
        List<RestaurantListModel> GetAll();
        RestaurantDetailModel? GetById(Guid id);
        Guid CreateOrUpdate(RestaurantCreateModel restaurantModel);
        Guid Create(RestaurantCreateModel restaurantModel);
        Guid? Update(RestaurantCreateModel restaurantModel);
        bool Delete(Guid id);
    }
}
