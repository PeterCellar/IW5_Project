using Microsoft.AspNetCore.Components;
using Delivery.Common.Models.Order;

namespace Delivery.Web.App.Pages
{
    public partial class OrderCreatePage
    {
        [Inject]
        private NavigationManager navigationManager { get; set; } = null!;

        [Parameter]
        public Guid RestaurantId { get; init; }

        public void NavigateBack()
        {
            navigationManager.NavigateTo("/restaurants/"+RestaurantId);
        }
    }
}
