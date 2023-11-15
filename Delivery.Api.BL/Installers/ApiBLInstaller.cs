using Delivery.Common.BL.Facades;
using Delivery.Common.Installers;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Api.BL.Installers
{
    public class ApiBLInstaller : IInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<ApiBLInstaller>()
                        .AddClasses(classes => classes.AssignableTo<IAppFacade>())
                        .AsSelfWithInterfaces()
                        .WithScopedLifetime());
        }
    }
}
