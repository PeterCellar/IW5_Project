using System;
using Microsoft.AspNetCore.Components;

namespace Delivery.Web.App.Pages
{
    public partial class DishCreatePage
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;

        [Parameter]
        public Guid RestaurantId { get; init; }

        public void NavigateBack()
        {
            NavigationManager.NavigateTo("/restaurants/"+RestaurantId);
        }
    }
}
