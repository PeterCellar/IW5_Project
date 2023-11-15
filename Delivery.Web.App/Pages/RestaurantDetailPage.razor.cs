using System;
using Delivery.Common.Models.Dish;
using Delivery.Common.Models.Restaurant;
using Delivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace Delivery.Web.App.Pages
{
    public partial class RestaurantDetailPage
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public RestaurantFacade RestaurantFacade { get; set; } = null!;

        [Inject]
        public DishFacade DishFacade { get; set; } = null!;

        private RestaurantDetailModel Restaurant { get; set; } = GetNewRestaurantModel();

        [Parameter]
        public Guid Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadData();

            await base.OnInitializedAsync();
        }

        private async Task LoadData()
        {
            Restaurant = await RestaurantFacade.GetByIdAsync(Id);
        }

        protected override async Task OnParametersSetAsync()
        {
            await OnInitializedAsync();
        }

        private static RestaurantDetailModel GetNewRestaurantModel()
            => new()
            {
                Id = Guid.NewGuid(),
                Name = string.Empty,
                Description = string.Empty,
                LogoUrl = string.Empty,
                Address = string.Empty,
                Latitude = 0,
                Longitude = 0,
            };

        public void NavigateBack()
        {
            NavigationManager.NavigateTo("/restaurants");
        }
    }
}
