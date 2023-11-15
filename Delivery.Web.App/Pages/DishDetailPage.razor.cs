using System;
using Delivery.Common.Enums;
using Delivery.Common.Models.Dish;
using Delivery.Common.Models.DishAllergen;
using Delivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace Delivery.Web.App.Pages
{
    public partial class DishDetailPage
    {
        [Inject]
        public DishFacade DishFacade { get; set; } = null!;

        public DishDetailModel Dish { get; set; } = GetNewDishModel();


        public bool IdExist { get; set; }
        public bool DishIdExist { get; set; }

        [Parameter]
        public Guid Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            IdExist = await DishFacade.Exist(Id);
            DishIdExist = await DishFacade.Exist(Dish.Id);
            await base.OnInitializedAsync();
        }

        private async Task LoadData()
        {
            Dish = await DishFacade.GetByIdAsync(Id);
        }

        protected override async Task OnParametersSetAsync()
        {
            await OnInitializedAsync();
        }

        private static DishDetailModel GetNewDishModel()
            => new()
            {
                Id = Guid.NewGuid(),
                Name = string.Empty,
                Description = string.Empty,
                ImageUrl = string.Empty,
                Price = 0,
                Allergens = new List<Allergen>(),
            };
    }
}
