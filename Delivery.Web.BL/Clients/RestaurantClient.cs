namespace Delivery.Web.BL
{
    public partial class RestaurantApiClient
    {
        public RestaurantApiClient(HttpClient httpClient, string baseUrl)
            : this(httpClient)
        {
            BaseUrl = baseUrl;
        }
    }
}
