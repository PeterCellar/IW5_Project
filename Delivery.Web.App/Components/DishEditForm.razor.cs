using System;
using AutoMapper;
using System.Threading.Tasks;
using Delivery.Common.Enums;
using Delivery.Common.Models.Dish;
using Delivery.Common.Models.DishAllergen;
using Delivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace Delivery.Web.App
{
    public partial class DishEditForm
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;
        
        [Inject]
        public DishFacade DishFacade { get; set; } = null!;
        
        [Inject]
        private IMapper mapper { get; set; } = null!;

        [Parameter]
        public Guid Id { get; init; }
        
        [Parameter]
        public Guid RestaurantId { get; init; }

        [Parameter]
        public EventCallback OnModification { get; set; }

        public DishCreateModel Data { get; set; } = GetNewDishModel();
        
        public DishAllergenCreateModel NewDishAllergenModel { get; set; } = GetNewNewDishAllergenModel();

        public IList<Allergen> NotUsedAllergens = Enum.GetValues(typeof(Allergen)).Cast<Allergen>().ToList();

        public Allergen RemovedAllergen { get; set; } = Allergen.None;

        protected override async Task OnInitializedAsync()
        {
            if (Id != Guid.Empty)
            {
                DishDetailModel detailModel = await DishFacade.GetByIdAsync(Id);
                Data  = mapper.Map<DishCreateModel>(detailModel);
                
                foreach (var a in Data.Allergens)
                {
                    NotUsedAllergens.Remove(a);
                }
                NotUsedAllergens.Remove(Allergen.None);
            }

            await base.OnInitializedAsync();
        }

        public async Task Save()
        {
            if (RestaurantId != Guid.Empty)
            {
                Data.RestaurantId = RestaurantId;
            }
            await DishFacade.SaveAsync(Data);
            
            NavigationManager.NavigateTo("/restaurants/"+Data.RestaurantId);
        }

        public async Task Delete()
        {
            await DishFacade.DeleteAsync(Data.Id);
            NavigationManager.NavigateTo("/restaurants/"+Data.RestaurantId);
        }

        private async Task NotifyOnModification()
        {
            if (OnModification.HasDelegate)
            {
                await OnModification.InvokeAsync(null);
            }
        }
        
        public void AddAllergen()
        {
            if (NewDishAllergenModel.Allergen != Allergen.None)
            {
                Allergen allergen = NewDishAllergenModel.Allergen;
                Data.Allergens.Add(allergen);
                NotUsedAllergens.Remove(allergen);
                NewDishAllergenModel = GetNewNewDishAllergenModel();
            }
        }
        
        public void RemoveAllergen()
        {
            if (RemovedAllergen != Allergen.None)
            {
                Allergen allergen = RemovedAllergen;
                Data.Allergens.Remove(allergen);
                NotUsedAllergens.Add(allergen);
                RemovedAllergen = Allergen.None;
            }
        }

        private static DishCreateModel GetNewDishModel()
            => new()
            {
                Id = Guid.NewGuid(),
                Name = string.Empty,
                Description = string.Empty,
                ImageUrl = string.Empty,
                Allergens = new List<Allergen>(),
                Price = 0
            };
        
        private static DishAllergenCreateModel GetNewNewDishAllergenModel()
            => new()
            {
                Id = Guid.NewGuid(),
                DishId = Guid.Empty,
                Allergen = Allergen.None
            };
    }
}
