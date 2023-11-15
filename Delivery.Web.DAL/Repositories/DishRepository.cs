using Delivery.Common.Models.Dish;

namespace Delivery.Web.DAL.Repositories
{
    public class DishRepository : RepositoryBase<DishCreateModel, DishDetailModel, DishListModel>
    {
        public override string TableName { get; } = "dishes";

        public DishRepository(LocalDb localDb)
            : base(localDb)
        {

        }
    }
}
