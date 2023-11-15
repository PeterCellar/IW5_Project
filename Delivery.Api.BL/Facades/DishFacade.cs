using AutoMapper;
using Delivery.Api.BL.Facades.Interfaces;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Api.DAL.Common.Repositories;
using Delivery.Common.Models.Dish;

namespace Delivery.Api.BL.Facades
{
    public class DishFacade : IDishFacade
    {
        private readonly IDishRepository dishRepository;
        private readonly IMapper mapper;

        public DishFacade(IDishRepository dishRepository, IMapper mapper)
        {
            this.dishRepository = dishRepository;
            this.mapper = mapper;
        }

        public Guid Create(DishCreateModel dishModel)
        {
            var dishEntity = mapper.Map<DishEntity>(dishModel);
            return dishRepository.Insert(dishEntity);
        }

        public Guid CreateOrUpdate(DishCreateModel dishModel)
        {
            return dishRepository.Exists(dishModel.Id)
                ? Update(dishModel)!.Value
                : Create(dishModel);
        }

        public bool Delete(Guid id)
        {
            return dishRepository.Remove(id);
        }

        public List<DishListModel> GetAll()
        {
            return mapper.Map<List<DishListModel>>(dishRepository.GetAll());
        }

        public DishDetailModel? GetById(Guid id)
        {
            var dishEntity = dishRepository.GetById(id);
            return mapper.Map<DishDetailModel>(dishEntity);
        }

        public Guid? Update(DishCreateModel dishModel)
        {
            var dishEntity = mapper.Map<DishEntity>(dishModel);
            return dishRepository.Update(dishEntity);
        }
    }
}
