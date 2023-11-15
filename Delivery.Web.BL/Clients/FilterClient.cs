namespace Delivery.Web.BL
{
    public partial class FilterApiClient
    {
        public FilterApiClient(HttpClient httpClient, string baseUrl)
            : this(httpClient)
        {
            BaseUrl = baseUrl;
        }
    }
}
