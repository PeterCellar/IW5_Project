using Delivery.Common.Models.Order;

namespace Delivery.Web.DAL.Repositories
{
    public class OrderRepository : RepositoryBase<OrderCreateModel, OrderDetailModel, OrderListModel>
    {
        public override string TableName { get; } = "orders";

        public OrderRepository(LocalDb localDb)
            : base(localDb)
        {

        }
    }
}
