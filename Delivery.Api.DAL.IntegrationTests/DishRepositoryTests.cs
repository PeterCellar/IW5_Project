using System;
using System.Collections.Generic;
using System.Linq;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Common.Enums;
using Xunit;

namespace Delivery.API.DAL.IntegrationTests;

public class DishRepositoryTests
{
    public DishRepositoryTests()
    {
        _dbFixture = new InMemoryDatabaseFixture();
    }

    private readonly IDatabaseFixture _dbFixture;

    [Fact]
    public void GetAll_Dishes()
    {
        //arrange
        var dishRepository = _dbFixture.GetDishRepository();

        //act
        var dishesCount = dishRepository.GetAll().Count();

        //assert
        Assert.Equal(3, dishesCount);
    }
    
    [Fact]
    public void Insert_Dish()
    {
        //arrange
        var dishRepository = _dbFixture.GetDishRepository();
        var dishId = Guid.NewGuid();

        var allergenList =
            new List<DishAllergenEntity>() { 
                new DishAllergenEntity(Guid.NewGuid(), Allergen.Eggs, dishId),
                new DishAllergenEntity(Guid.NewGuid(), Allergen.Milk, dishId)
            };

        var dish = new DishEntity(dishId, "Pancakes", "Chocolate", 2, _dbFixture.RestaurantGuids[0])
        {
            Allergens = allergenList
        };
        
        //act
        dishRepository.Insert(dish);

        //assert
        var dishFromDb = _dbFixture.GetDishDirectly(dishId);
        Assert.NotNull(dishFromDb);
        Assert.Equal(allergenList.Count, dishFromDb.Allergens.Count());
    }
    
    [Fact]
    public void Insert_Than_Remove_Dish()
    {
        //arrange
        var dishRepository = _dbFixture.GetDishRepository();

        var dishId = Guid.NewGuid();
        var dish = new DishEntity(dishId, "Cake", "Chocolate", 9, _dbFixture.RestaurantGuids[0]);
        
        //act
        dishRepository.Insert(dish);

        //assert
        var dishFromDb = _dbFixture.GetDishDirectly(dishId);
        Assert.NotNull(dishFromDb);
        Assert.Equal(dish.Name, dishFromDb.Name);
        
        //act
        dishRepository.Remove(dishId);
        
        //assert
        var removedDishFromDb = _dbFixture.GetOrderDirectly(dishId);
        Assert.Null(removedDishFromDb);
    }
    
    [Fact]
    public void Exist_Dish()
    {
        //arrange
        var dishRepository = _dbFixture.GetDishRepository();
        var dishId = _dbFixture.DishGuids[0];
        var unknownId = Guid.NewGuid();

        //assert
        Assert.True(dishRepository.Exists(dishId));
        Assert.False(dishRepository.Exists(unknownId));
    }
}
