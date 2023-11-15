using Delivery.Common.Models.Restaurant;

namespace Delivery.Web.DAL.Repositories
{
    public class RestaurantRepository : RepositoryBase<RestaurantCreateModel, RestaurantDetailModel, RestaurantListModel>
    {
        public override string TableName { get; } = "restaurants";

        public RestaurantRepository(LocalDb localDb)
            : base(localDb)
        {

        }
    }
}
