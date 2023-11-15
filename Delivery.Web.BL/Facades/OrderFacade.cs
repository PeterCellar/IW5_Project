using AutoMapper;
using Delivery.Common.Models.Order;
using Delivery.Web.BL.Options;
using Delivery.Web.DAL.Repositories;
using Microsoft.Extensions.Options;

namespace Delivery.Web.BL.Facades
{
    public class OrderFacade : FacadeBase<OrderCreateModel, OrderDetailModel, OrderListModel>
    {
        private readonly IOrderApiClient apiClient;
        private readonly IMapper mapper;

        public OrderFacade(
            IOrderApiClient apiClient,
            OrderRepository orderRepository,
            IMapper mapper,
            IOptions<LocalDbOptions> localDbOptions)
                : base(orderRepository, mapper, localDbOptions)
        {
            this.apiClient = apiClient;
            this.mapper = mapper;
        }

        public override async Task<List<OrderListModel>> GetAllAsync()
        {
            var ordersAll = await base.GetAllAsync();

            var ordersFromApi = await apiClient.OrderGetAsync();
            ordersAll.AddRange(ordersFromApi);

            return ordersAll;
        }

        public override async Task DeleteAsync(Guid id)
        {
            await apiClient.OrderDeleteAsync(id);
        }

        public override async Task<OrderDetailModel> GetByIdAsync(Guid id)
        {
            return await apiClient.OrderGetAsync(id);
        }

        public override async Task<bool> SynchronizeLocalDataAsync()
        {
            var localItems = await repository.GetAllDetailAsync();
            OrderCreateModel createItem;

            foreach (var localItem in localItems)
            {
                createItem = mapper.Map<OrderCreateModel>(localItem);
                await SaveToApiAsync(createItem);
                await repository.RemoveAsync(localItem.Id);
            }

            return localItems.Any();
        }

        public override async Task<Guid> SaveToApiAsync(OrderCreateModel data)
        {
            if (await Exist(data.Id))
                return await apiClient.OrderPatchAsync(data);

            return await apiClient.OrderPostAsync(data);
        }
    }
}
