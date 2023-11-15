namespace Delivery.Web.BL
{
    public partial class OrderApiClient
    {
        public OrderApiClient(HttpClient httpClient, string baseUrl)
            : this(httpClient)
        {
            BaseUrl = baseUrl;
        }
    }
}
