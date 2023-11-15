using System;
using System.Collections.Generic;
using System.Linq;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Common.Enums;
using Xunit;

namespace Delivery.API.DAL.IntegrationTests;

public class RestaurantRepositoryTests
{
    public RestaurantRepositoryTests()
    {
        _dbFixture = new InMemoryDatabaseFixture();
    }

    private readonly IDatabaseFixture _dbFixture;

    [Fact]
    public void GetAll_Restaurants()
    {
        //arrange
        var restaurantRepository = _dbFixture.GetRestaurantRepository();

        //act
        var restaurantsCount = restaurantRepository.GetAll().Count();

        //assert
        Assert.Equal(2, restaurantsCount);
    }

    [Fact]
    public void Insert_Restaurant()
    {
        //arrange
        var restaurantRepository = _dbFixture.GetRestaurantRepository();

        var dishList = _dbFixture.GetDishRepository().GetAll();
        
        var restaurantId = Guid.NewGuid();
        var restaurantDishes = new List<DishEntity>(){dishList[0], dishList[2]};
        var restaurant = new RestaurantEntity(restaurantId, "LuxuryRestaurant", "Expensive", "14 London Road", 15.74586, 84.45612)
        {
            Dishes = restaurantDishes
        };
        
        //act
        restaurantRepository.Insert(restaurant);

        //assert
        var restaurantFromDb = _dbFixture.GetRestaurantDirectly(restaurantId);
        Assert.NotNull(restaurantFromDb);
        Assert.Equal(restaurantDishes.Count, restaurantFromDb!.Dishes.Count);
    }
    
    [Fact]
    public void Insert_Than_Remove_Restaurant()
    {
        //arrange
        var restaurantRepository = _dbFixture.GetRestaurantRepository();

        var dishList = _dbFixture.GetDishRepository().GetAll();
        
        var restaurantId = Guid.NewGuid();
        var restaurantDishes = new List<DishEntity>(){dishList[1], dishList[2]};
        var restaurant = new RestaurantEntity(restaurantId, "NormalRestaurant", "NothingSpecial", "16 London Road", 15.74587, 84.45612)
        {
            Dishes = restaurantDishes
        };
        
        //act
        restaurantRepository.Insert(restaurant);

        //assert
        var restaurantFromDb = _dbFixture.GetRestaurantDirectly(restaurantId);
        Assert.NotNull(restaurantFromDb);
        Assert.Equal(restaurantDishes.Count, restaurantFromDb!.Dishes.Count);
        
        //act
        restaurantRepository.Remove(restaurantId);
        
        //assert
        var removedRestaurantFromDb = _dbFixture.GetOrderDirectly(restaurantId);
        Assert.Null(removedRestaurantFromDb);
    }
    
    [Fact]
    public void Exist_Restaurant()
    {
        //arrange
        var restaurantRepository = _dbFixture.GetRestaurantRepository();
        var restaurantId = _dbFixture.RestaurantGuids[1];
        var unknownId = Guid.NewGuid();

        //assert
        Assert.True(restaurantRepository.Exists(restaurantId));
        Assert.False(restaurantRepository.Exists(unknownId));
    }
}
