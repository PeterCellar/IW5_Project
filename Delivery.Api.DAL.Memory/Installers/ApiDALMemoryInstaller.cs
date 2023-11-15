using Delivery.Api.DAL.Common.Repositories;
using Delivery.Api.DAL.Memory.Repositories;
using Delivery.Common.Installers;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Api.DAL.Memory.Installers
{
    public class ApiDALMemoryInstaller : IInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<ApiDALMemoryInstaller>()
                        .AddClasses(classes => classes.AssignableTo(typeof(IApiRepository<>)))
                            .AsMatchingInterface()
                            .WithTransientLifetime()
                        .AddClasses(classes => classes.AssignableTo<Storage>())
                            .AsSelf()
                            .WithSingletonLifetime()
            );
        }
    }
}
