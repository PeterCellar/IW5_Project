using System.Net.Http.Json;
using Delivery.Common.Models;
using Delivery.Common.Models.Order;
using Delivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Delivery.Web.App
{
    public partial class SearchBar
    {
        [Inject]
        private HttpClient Http { get; set; } = null!;
        [Inject]
        private IConfiguration Configuration { get; set; } = null!;
        private string FilterString { get; set; } = string.Empty;
        private bool ShowResults { get; set; } = false;

        private IList<FilterModel> FilterList { get; set; } = new List<FilterModel>();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        private async Task SearchAsync()
        {
            FilterList = await fetchFilter();
            ShowResults = true;
        }

        private async Task<IList<FilterModel>> fetchFilter()
        {
            var apiBaseUrl = Configuration.GetValue<string>("ApiBaseUrl");
            var httpResponse = await Http.GetAsync($"{apiBaseUrl}/filter/{FilterString}");

            if (!httpResponse.IsSuccessStatusCode)
            {
                var errorMessage = httpResponse.ReasonPhrase;
                Console.WriteLine($"There was an error! {errorMessage}");
                return new List<FilterModel>();
            }

            List<FilterModel> response = new List<FilterModel>();
            var responseContent = await httpResponse.Content.ReadFromJsonAsync<List<FilterModel>>();

            if (responseContent != null)
            {
                response = responseContent;
            }

            return response;
        }

        private void Hide()
        {
            ShowResults = false;
            FilterString = string.Empty;
        }

        private async Task OnInput(ChangeEventArgs e)
        {
            FilterString = e.Value?.ToString() ?? string.Empty;
            await SearchAsync();
        }
    }
}
