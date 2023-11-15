using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Delivery.Api.DAL.Memory;
using Delivery.API.DAL.IntegrationTests;
using Delivery.Common.Enums;
using Delivery.Common.Models.Dish;
using Delivery.Common.Models.OrderDish;
using Delivery.Common.Models.Restaurant;
using Newtonsoft.Json;
using Xunit;

namespace Delivery.Api.App.EndToEndTests
{
    public class DishControllerTests : IAsyncDisposable
    {
        private readonly DeliveryApiApplicationFactory application;
        private readonly Lazy<HttpClient> client;

        public DishControllerTests()
        {
            application = new DeliveryApiApplicationFactory();
            client = new Lazy<HttpClient>(application.CreateClient());
        }

        [Fact]
        public async Task GetAllDishes_Returns_At_Last_One_Dish()
        {
            var response = await client.Value.GetAsync("/Dish");

            response.EnsureSuccessStatusCode();

            var dishes = await response.Content.ReadFromJsonAsync<ICollection<DishListModel>>();
            Assert.NotNull(dishes);
            Assert.NotEmpty(dishes);
        }

        [Fact]
        public async Task GetDishById_Returns_Correct_Dish()
        {
            var response = await client.Value.GetAsync("/Dish");
            response.EnsureSuccessStatusCode();

            var dishes = await response.Content.ReadFromJsonAsync<ICollection<DishDetailModel>>();
            var dishSeed = dishes!.First();

            response = await client.Value.GetAsync("/Dish/" + dishSeed.Id.ToString());
            response.EnsureSuccessStatusCode();

            var dish = await response.Content.ReadFromJsonAsync<DishDetailModel>();
            Assert.NotNull(dish);
            Assert.Equal(dishSeed.Name, dish.Name);
        }

        [Fact]
        public async Task DeleteDishById_Delete_Correct_Dish()
        {
            var response = await client.Value.GetAsync("/Dish");
            response.EnsureSuccessStatusCode();

            var dishes = await response.Content.ReadFromJsonAsync<ICollection<DishListModel>>();
            Assert.NotNull(dishes);
            var dishDelete = dishes.First();

            response = await client.Value.DeleteAsync("/Dish?id=" + dishDelete.Id.ToString());
            response.EnsureSuccessStatusCode();

            response = await client.Value.GetAsync("/Dish");
            response.EnsureSuccessStatusCode();

            var dishes2 = await response.Content.ReadFromJsonAsync<ICollection<DishListModel>>();
            Assert.NotNull(dishes2);

            var dish2 = dishes2.First();

            Assert.Equal(dishes.Count - 1, dishes2.Count);
            Assert.NotEqual(dishDelete.Id, dish2.Id);
        }

        [Fact]
        public async Task UpdateDish_Update_Correct_Dish()
        {
            var dishListResponse = await client.Value.GetAsync("/Dish");
            dishListResponse.EnsureSuccessStatusCode();

            var dishList = await dishListResponse.Content.ReadFromJsonAsync<ICollection<DishListModel>>();
            Assert.NotNull(dishList);

            var dishResponse = await client.Value.GetAsync("/Dish/" + dishList.First().Id.ToString());
            dishResponse.EnsureSuccessStatusCode();

            var dish = await dishResponse.Content.ReadFromJsonAsync<DishDetailModel>();
            Assert.NotNull(dish);

            var dishToUpdate = new DishCreateModel()
            {
                Id = dish.Id,
                Name = "Updated Dish",
                Description = "Updated Description",
                Price = 100,
                RestaurantId = dish.Restaurant!.Id,
                Allergens = dish.Allergens,
                ImageUrl = dish.ImageUrl
            };

            var json = JsonConvert.SerializeObject(dishToUpdate);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var updateResponse = await client.Value.PatchAsync("/Dish", httpContent);
            updateResponse.EnsureSuccessStatusCode();

            var detailResponse = await client.Value.GetAsync("/Dish/" + dishToUpdate.Id);
            detailResponse.EnsureSuccessStatusCode();

            var dishDetail = await detailResponse.Content.ReadFromJsonAsync<DishDetailModel>();
            Assert.NotNull(dishDetail);

            Assert.Equal(dishToUpdate.Id, dishDetail.Id);
            Assert.Equal(dishToUpdate.Name, dishDetail.Name);
        }

        [Fact]
        public async Task CreateDish_Create_Correctly()
        {
            var createdDish = new DishCreateModel()
            {
                Id = Guid.NewGuid(),
                Name = "CreateTestRestaurant",
                Description = "CreateTest",
                Price = 4,
                RestaurantId = Guid.Parse("cff8b2a5-2ddb-4584-b3fe-101a13956d4c"),
                Allergens = new List<Allergen>() { Allergen.Fish }
            };

            var json = JsonConvert.SerializeObject(createdDish);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.Value.PostAsync("/Dish", httpContent);
            response.EnsureSuccessStatusCode();

            response = await client.Value.GetAsync("/Dish");
            response.EnsureSuccessStatusCode();

            var dishes = await response.Content.ReadFromJsonAsync<ICollection<DishListModel>>();
            var dish = dishes?.Last();

            Assert.NotNull(dish);
            Assert.Equal(createdDish.Name, dish.Name);
        }

        public async ValueTask DisposeAsync()
        {
            await application.DisposeAsync();
        }
    }
}
