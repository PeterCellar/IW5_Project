using AutoMapper;
using Delivery.Api.BL.Facades;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Api.DAL.Common.Repositories;
using Delivery.Common.Models.Restaurant;
using Moq;

namespace Delivery.Api.BL.UnitTests
{
    public class RestaurantFacadeTests
    {
        [Fact]
        public void Delete_Calls_Correct_Method_On_Repository()
        {
            var repositoryMock = new Mock<IRestaurantRepository>(MockBehavior.Loose);
            repositoryMock.Setup(restaurantRepository => restaurantRepository.Remove(It.IsAny<Guid>()));

            var repository = repositoryMock.Object;
            var mapper = new Mock<IMapper>(MockBehavior.Loose).Object;
            var facade = new RestaurantFacade(repository, mapper);

            var itemId = Guid.NewGuid();

            facade.Delete(itemId);

            repositoryMock.Verify(restaurantRepository => restaurantRepository.Remove(itemId));
        }

        [Fact]
        public void GetAll_Calls_Correct_Method_On_Repository()
        {
            var repositoryMock = new Mock<IRestaurantRepository>(MockBehavior.Loose);
            repositoryMock.Setup(restaurantRepository => restaurantRepository.GetAll());

            var repository = repositoryMock.Object;
            var mapper = new Mock<IMapper>(MockBehavior.Loose).Object;
            var facade = new RestaurantFacade(repository, mapper);

            facade.GetAll();

            repositoryMock.Verify(restaurantRepository => restaurantRepository.GetAll());
        }

        [Fact]
        public void GetById_Calls_Correct_Method_On_Repository()
        {
            var repositoryMock = new Mock<IRestaurantRepository>(MockBehavior.Loose);
            repositoryMock.Setup(restaurantRepository => restaurantRepository.GetById(It.IsAny<Guid>()));

            var repository = repositoryMock.Object;
            var mapper = new Mock<IMapper>(MockBehavior.Loose).Object;
            var facade = new RestaurantFacade(repository, mapper);

            var itemId = Guid.NewGuid();
            facade.GetById(itemId);

            repositoryMock.Verify(restaurantRepository => restaurantRepository.GetById(itemId));
        }

        [Fact]
        public void Create_Calls_Correct_Method_On_Repository()
        {
            var repositoryMock = new Mock<IRestaurantRepository>(MockBehavior.Loose);
            repositoryMock.Setup(restaurantRepository => restaurantRepository.Insert(It.IsAny<RestaurantEntity>()));

            var repository = repositoryMock.Object;
            var mapper = new Mock<IMapper>(MockBehavior.Loose).Object;
            var facade = new RestaurantFacade(repository, mapper);

            var restaurantModel = new RestaurantCreateModel()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Description = "TestTestTestTest",
                Address = "Test",
                Latitude = 10.11616,
                Longitude = 4545.5656
            };

            facade.Create(restaurantModel);

            var restaurantEntity = mapper.Map<RestaurantEntity>(restaurantModel);
            repositoryMock.Verify(restaurantRepository => restaurantRepository.Insert(restaurantEntity));
        }

        [Fact]
        public void Update_Calls_Correct_Method_On_Repository()
        {
            var repositoryMock = new Mock<IRestaurantRepository>(MockBehavior.Loose);
            repositoryMock.Setup(restaurantRepository => restaurantRepository.Update(It.IsAny<RestaurantEntity>()));

            var repository = repositoryMock.Object;
            var mapper = new Mock<IMapper>(MockBehavior.Loose).Object;
            var facade = new RestaurantFacade(repository, mapper);

            var restaurantModel = new RestaurantCreateModel()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Description = "TestTestTestTest",
                Address = "Test",
                Latitude = 10.11616,
                Longitude = 4545.5656
            };

            facade.Update(restaurantModel);

            var restaurantEntity = mapper.Map<RestaurantEntity>(restaurantModel);
            repositoryMock.Verify(restaurantRepository => restaurantRepository.Update(restaurantEntity));
        }
    }
}
