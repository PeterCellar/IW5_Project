using AutoMapper;
using Delivery.Api.BL.Facades.Interfaces;
using Delivery.Api.DAL.Common.Repositories;
using Delivery.Common.Models;

namespace Delivery.Api.BL.Facades
{
    public class FilterFacade : IFilterFacade
    {
        private readonly IRestaurantRepository restaurantRepository;
        private readonly IDishRepository dishRepository;
        private readonly IMapper mapper;

        public FilterFacade(IRestaurantRepository restaurantRepository, IDishRepository dishRepository, IMapper mapper)
        {
            this.restaurantRepository = restaurantRepository;
            this.dishRepository = dishRepository;
            this.mapper = mapper;
        }

        public List<FilterModel> GetBySubstring(string substring)
        {
            var restaurantEntities = restaurantRepository.GetBySubstring(substring);
            var dishEntities = dishRepository.GetBySubstring(substring);

            var restaurantModels = mapper.Map<List<FilterModel>>(restaurantEntities);
            var dishModels = mapper.Map<List<FilterModel>>(dishEntities);

            return dishModels.Concat(restaurantModels).ToList();
        }


    }
}
