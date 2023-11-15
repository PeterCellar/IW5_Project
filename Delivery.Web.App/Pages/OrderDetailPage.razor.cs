using Delivery.Common.Models.Dish;
using Delivery.Common.Models.Order;
using Delivery.Common.Models.OrderDish;
using Delivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace Delivery.Web.App.Pages
{
    public partial class OrderDetailPage
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public OrderFacade orderFacade { get; set; } = null!;

        [Parameter]
        public Guid Id { get; set; }

        private OrderDetailModel Order { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            await base.OnInitializedAsync();
        }

        private async Task LoadData()
        {
            Order = await orderFacade.GetByIdAsync(Id);
        }

        public void NavigateBack()
        {
            NavigationManager.NavigateTo("/orders");
        }
    }
}
