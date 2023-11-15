using System;
using Microsoft.AspNetCore.Components;

namespace Delivery.Web.App.Pages
{
    public partial class DishEditPage
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
