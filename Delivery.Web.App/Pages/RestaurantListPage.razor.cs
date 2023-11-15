using System.Collections.Generic;
using System.Threading.Tasks;
using Delivery.Common.Models.Restaurant;
using Delivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace Delivery.Web.App.Pages
{
    public partial class RestaurantListPage
    {
        [Inject]
        public RestaurantFacade RestaurantFacade { get; set; } = null!;

        private ICollection<RestaurantListModel> Restaurants { get; set; } = new List<RestaurantListModel>();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();

            await base.OnInitializedAsync();
        }

        private async Task LoadData()
        {
            Restaurants = await RestaurantFacade.GetAllAsync();
        }
    }
}
