using AutoMapper;
using Delivery.Api.BL.Facades;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Api.DAL.Common.Repositories;
using Delivery.Common.Enums;
using Delivery.Common.Models.Dish;
using Delivery.Common.Models.Restaurant;
using Moq;

namespace Delivery.Api.BL.UnitTests
{
    public class DishFacadeTests
    {
        private static DishFacade GetFacadeWithForbiddenDependencyCalls()
        {
            var repository = new Mock<IDishRepository>(MockBehavior.Strict).Object;
            var mapper = new Mock<IMapper>(MockBehavior.Strict).Object;
            var facade = new DishFacade(repository, mapper);
            return facade;
        }

        [Fact]
        public void Delete_Calls_Correct_Method_On_Repository()
        {
            var repositoryMock = new Mock<IDishRepository>(MockBehavior.Loose);
            repositoryMock.Setup(dishRepository => dishRepository.Remove(It.IsAny<Guid>()));

            var repository = repositoryMock.Object;
            var mapper = new Mock<IMapper>(MockBehavior.Loose).Object;
            var facade = new DishFacade(repository, mapper);

            var itemId = Guid.NewGuid();

            facade.Delete(itemId);

            repositoryMock.Verify(dishRepository => dishRepository.Remove(itemId));
        }

        [Fact]
        public void GetAll_Calls_Correct_Method_On_Repository()
        {
            var repositoryMock = new Mock<IDishRepository>(MockBehavior.Loose);
            repositoryMock.Setup(dishRepository => dishRepository.GetAll());

            var repository = repositoryMock.Object;
            var mapper = new Mock<IMapper>(MockBehavior.Loose).Object;
            var facade = new DishFacade(repository, mapper);

            facade.GetAll();

            repositoryMock.Verify(dishRepository => dishRepository.GetAll());
        }

        [Fact]
        public void GetById_Calls_Correct_Method_On_Repository()
        {
            var repositoryMock = new Mock<IDishRepository>(MockBehavior.Loose);
            repositoryMock.Setup(dishRepository => dishRepository.GetById(It.IsAny<Guid>()));

            var repository = repositoryMock.Object;
            var mapper = new Mock<IMapper>(MockBehavior.Loose).Object;
            var facade = new DishFacade(repository, mapper);

            var itemId = Guid.NewGuid();
            facade.GetById(itemId);

            repositoryMock.Verify(dishRepository => dishRepository.GetById(itemId));
        }

        [Fact]
        public void Create_Calls_Correct_Method_On_Repository()
        {
            var repositoryMock = new Mock<IDishRepository>(MockBehavior.Loose);
            repositoryMock.Setup(dishRepository => dishRepository.Insert(It.IsAny<DishEntity>()));

            var repository = repositoryMock.Object;
            var mapper = new Mock<IMapper>(MockBehavior.Loose).Object;
            var facade = new DishFacade(repository, mapper);

            var dishModel = new DishCreateModel()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Description = "TestTestTestTest",
                Price = Convert.ToDecimal(100.55),
                Allergens = new List<Allergen>()
                {
                    Allergen.SesameSeeds
                }
            };

            facade.Create(dishModel);

            var dishEntity = mapper.Map<DishEntity>(dishModel);
            repositoryMock.Verify(dishRepository => dishRepository.Insert(dishEntity));
        }

        [Fact]
        public void Update_Calls_Correct_Method_On_Repository()
        {
            var repositoryMock = new Mock<IDishRepository>(MockBehavior.Loose);
            repositoryMock.Setup(dishRepository => dishRepository.Update(It.IsAny<DishEntity>()));

            var repository = repositoryMock.Object;
            var mapper = new Mock<IMapper>(MockBehavior.Loose).Object;
            var facade = new DishFacade(repository, mapper);

            var dishModel = new DishCreateModel()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Description = "TestTestTestTest",
                Price = Convert.ToDecimal(100.55),
                Allergens = new List<Allergen>()
                {
                    Allergen.SesameSeeds
                }
            };

            facade.Update(dishModel);

            var dishEntity = mapper.Map<DishEntity>(dishModel);
            repositoryMock.Verify(dishRepository => dishRepository.Update(dishEntity));
        }
    }
}
