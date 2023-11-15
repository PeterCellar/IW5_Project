using AutoMapper;
using Delivery.Common.Models.Dish;
using Delivery.Web.BL.Options;
using Delivery.Web.DAL.Repositories;
using Microsoft.Extensions.Options;

namespace Delivery.Web.BL.Facades
{
    public class DishFacade : FacadeBase<DishCreateModel, DishDetailModel, DishListModel>
    {
        private readonly IDishApiClient apiClient;

        public DishFacade(
            IDishApiClient apiClient,
            DishRepository dishRepository,
            IMapper mapper,
            IOptions<LocalDbOptions> localDbOptions)
            : base(dishRepository, mapper, localDbOptions)
        {
            this.apiClient = apiClient;
        }

        public override async Task<List<DishListModel>> GetAllAsync()
        {
            var dishesAll = await base.GetAllAsync();

            var dishesFromApi = await apiClient.DishGetAsync();
            dishesAll.AddRange(dishesFromApi);

            return dishesAll;
        }

        public override async Task<DishDetailModel> GetByIdAsync(Guid id)
        {
            return await apiClient.DishGetAsync(id);
        }

        public override async Task<Guid> SaveToApiAsync(DishCreateModel data)
        {
            var alreadyExist = await Exist(data.Id);
            if (alreadyExist)
                return await apiClient.DishPatchAsync(data);
            else
                return await apiClient.DishPostAsync(data);
        }

        public override async Task DeleteAsync(Guid id)
        {
            await apiClient.DishDeleteAsync(id);
        }

        public override async Task<bool> SynchronizeLocalDataAsync()
        {
            var localItems = await repository.GetAllDetailAsync();
            foreach (var localItem in localItems)
            {
                await SaveToApiAsync(localItem);
                await repository.RemoveAsync(localItem.Id);
            }

            return localItems.Any();
        }
    }
}
