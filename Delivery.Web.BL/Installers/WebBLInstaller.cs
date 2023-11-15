
using Delivery.Common.BL.Facades;
using Delivery.Web.BL;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Web.BL.Installers
{
    public class WebBLInstaller
    {
        public void Install(IServiceCollection serviceCollection, string apiBaseUrl)
        {
            serviceCollection.AddTransient<IDishApiClient, DishApiClient>(provider =>
            {
                var client = CreateApiHttpClient(provider, apiBaseUrl);
                return new DishApiClient(client, apiBaseUrl);
            });

            serviceCollection.AddTransient<IOrderApiClient, OrderApiClient>(provider =>
            {
                var client = CreateApiHttpClient(provider, apiBaseUrl);
                return new OrderApiClient(client, apiBaseUrl);
            });

            serviceCollection.AddTransient<IRestaurantApiClient, RestaurantApiClient>(provider =>
            {
                var client = CreateApiHttpClient(provider, apiBaseUrl);
                return new RestaurantApiClient(client, apiBaseUrl);
            });

            serviceCollection.AddTransient<IFilterApiClient, FilterApiClient>(provider =>
            {
                var client = CreateApiHttpClient(provider, apiBaseUrl);
                return new FilterApiClient(client, apiBaseUrl);
            });

            serviceCollection.AddTransient<IRevenueApiClient, RevenueApiClient>(provider =>
            {
                var client = CreateApiHttpClient(provider, apiBaseUrl);
                return new RevenueApiClient(client, apiBaseUrl);
            });

            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<WebBLInstaller>()
                    .AddClasses(classes => classes.AssignableTo<IAppFacade>())
                    .AsSelfWithInterfaces()
                    .WithTransientLifetime());
        }

        public HttpClient CreateApiHttpClient(IServiceProvider serviceProvider, string apiBaseUrl)
        {
            var client = new HttpClient() { BaseAddress = new Uri(apiBaseUrl) };
            client.BaseAddress = new Uri(apiBaseUrl);
            return client;
        }
    }
}
