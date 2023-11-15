namespace Delivery.Web.BL
{
    public partial class DishApiClient
    {
        public DishApiClient(HttpClient httpClient, string baseUrl)
            : this(httpClient)
        {
            BaseUrl = baseUrl;
        }
    }
}
