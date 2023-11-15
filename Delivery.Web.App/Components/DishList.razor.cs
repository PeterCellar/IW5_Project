using System;
using System.Threading.Tasks;
using Delivery.Common.Models.Dish;
using Microsoft.AspNetCore.Components;

namespace Delivery.Web.App
{
    public partial class DishList
    {
        [Parameter]
        public ICollection<DishListModel> Dishes { get; set; } = new List<DishListModel>();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }
    }
}
