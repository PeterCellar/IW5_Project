using Delivery.Common.BL.Facades;
using Delivery.Common.Models.Order;

namespace Delivery.Api.BL.Facades.Interfaces
{
    public interface IOrderFacade : IAppFacade
    {
        List<OrderListModel> GetAll();
        OrderDetailModel? GetById(Guid id);
        Guid CreateOrUpdate(OrderCreateModel orderModel);
        Guid Create(OrderCreateModel orderModel);
        Guid? Update(OrderCreateModel orderModel);
        bool Delete(Guid id);
    }
}
