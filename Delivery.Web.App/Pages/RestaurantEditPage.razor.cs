using System;
using Delivery.Common.Models.Restaurant;
using Microsoft.AspNetCore.Components;

namespace Delivery.Web.App.Pages
{
    public partial class RestaurantEditPage
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;

        [Parameter]
        public Guid Id { get; init; }

        public void NavigateBack()
        {
            NavigationManager.NavigateTo("/restaurants");
        }
    }
}
