namespace Delivery.Web.BL
{
    public partial class RevenueApiClient
    {
        public RevenueApiClient(HttpClient httpClient, string baseUrl)
            : this(httpClient)
        {
            BaseUrl = baseUrl;
        }
    }
}
