using Delivery.Common.Models.Order;
using Delivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace Delivery.Web.App
{
    public partial class OrderList
    {
        [Parameter]
        public IList<OrderListModel> Orders { get; set; } = new List<OrderListModel>();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }
    }
}
