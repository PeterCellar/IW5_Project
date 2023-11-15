using AutoMapper;
using Delivery.Common.Models.Restaurant;
using Delivery.Web.BL.Options;
using Delivery.Web.DAL.Repositories;
using Microsoft.Extensions.Options;

namespace Delivery.Web.BL.Facades
{
    public class RestaurantFacade : FacadeBase<RestaurantCreateModel, RestaurantDetailModel, RestaurantListModel>
    {
        private readonly IRestaurantApiClient apiClient;
        private readonly IMapper mapper;

        public RestaurantFacade(
            IRestaurantApiClient apiClient,
            RestaurantRepository restaurantRepository,
            IMapper mapper,
            IOptions<LocalDbOptions> localDbOptions)
            : base(restaurantRepository, mapper, localDbOptions)
        {
            this.apiClient = apiClient;
            this.mapper = mapper;
        }

        public override async Task<List<RestaurantListModel>> GetAllAsync()
        {
            var restaurantsAll = await base.GetAllAsync();

            var restaurantsFromApi = await apiClient.RestaurantGetAsync();
            foreach (var restaurantFromApi in restaurantsFromApi)
            {
                if (restaurantsAll.Any(r => r.Id == restaurantFromApi.Id) is false)
                {
                    restaurantsAll.Add(restaurantFromApi);
                }
            }

            return restaurantsAll;
        }

        public override async Task<RestaurantDetailModel> GetByIdAsync(Guid id)
        {
            return await apiClient.RestaurantGetAsync(id);
        }

        public override async Task<Guid> SaveToApiAsync(RestaurantCreateModel data)
        {
            var alreadyExist = await Exist(data.Id);
            if (alreadyExist)
                return await apiClient.RestaurantPatchAsync(data);
            else
                return await apiClient.RestaurantPostAsync(data);
        }

        public override async Task DeleteAsync(Guid id)
        {
            await apiClient.RestaurantDeleteAsync(id);
        }

        public override async Task<bool> SynchronizeLocalDataAsync()
        {
            var localItems = await repository.GetAllDetailAsync();
            foreach(var localItem in localItems)
            {
                RestaurantCreateModel createItem = mapper.Map<RestaurantCreateModel>(localItem);
                await SaveToApiAsync(createItem);
                await repository.RemoveAsync(localItem.Id);

            }

            return localItems.Any();
        }
    }
}
