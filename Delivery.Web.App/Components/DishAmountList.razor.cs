using Delivery.Common.Models.OrderDish;
using Microsoft.AspNetCore.Components;

namespace Delivery.Web.App
{
    public partial class DishAmountList
    {
        [Parameter]
        public IList<OrderDishDetailModel> DishAmounts { get; set; } = new List<OrderDishDetailModel>();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }
    }
}
